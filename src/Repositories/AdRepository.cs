﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using ActiveDirectory.Models.Entities;
using Microsoft.Extensions.Logging;

namespace ActiveDirectory.Repositories;

public class AdRepository : IAdRepository
{
    private readonly ILogger<AdRepository> logger;

    public IEnumerable<string> Domains;

    public AdRepository(ILogger<AdRepository> logger) : this(logger, new string[] { Domain.GetCurrentDomain().Name })
    {
    }

    public AdRepository(ILogger<AdRepository> logger, IEnumerable<string> domains)
    {
        this.logger = logger;
        try
        {
            Domains = domains.Select(x => ToLDAP(x));
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "A critical exception doesnt allow to load the correct dependencies");
        }
    }

    public (bool, string) AuthenticateUser(string userName, string password)
    {
        (bool allowed, string message) resp = (false, string.Empty);

        foreach (string x in Domains)
        {
            resp = AuthenticateUser(userName, password, x);

            if (resp.allowed)
                return resp;
        }

        return resp;
    }

    public (bool, string) AuthenticateUser(string userName, string password, string domain)
    {
        try
        {
            if (domain is null)
            {
                domain = Domains.FirstOrDefault();
            }
            else
            {
                domain = ToLDAP(domain);
            }

            var de = new DirectoryEntry()
            {
                Path = domain,
                AuthenticationType = AuthenticationTypes.Secure,
                Username = userName,
                Password = password
            };

            using var search = new DirectorySearcher(de);
            var result = search.FindOne();

            if (result is not null)
            {
                return (true, string.Empty);
            }
        }
        catch (Exception x)
        {
            logger.LogWarning(x, "Method: Authenticate user logged an exception");
            return (false, x.Message);
        }

        return (false, "User not Found.");
    }

    public IEnumerable<User> GetGroupUsers(string group)
    {
        IEnumerable<User> GetGroupUsers(string domain)
        {
            const string member = nameof(member);
            var response = new List<User>();

            //Create a search by Group Name
            using var search = new DirectorySearcher(new DirectoryEntry(domain), $"(cn={group})");

            //Get the users in the group
            search.PropertiesToLoad.Add(member);
            var result = search.FindOne();
            if (result is not null)
            {
                int usersCount = result.Properties[member].Count;

                for (int counter = 0; counter < usersCount; counter++)
                {
                    var user = GetUser(result.Properties[member][counter].ToString());

                    if (user is not null && !string.IsNullOrEmpty(user.UserName))
                    {
                        user.Group = group;
                        response.Add(user);
                    }
                }
            }

            return response;
        }

        return Domains.AsParallel().Select(x => GetGroupUsers(x)).SelectMany(y => y);
    }

    public IEnumerable<UserGroup> GetUserGroups(string userName)
    {
        IEnumerable<UserGroup> GetUserGroups(string domain)
        {
            const string memberOf = nameof(memberOf);
            var userGroups = new List<UserGroup>();
            //Strip the Domain Name from userName if included
            string strippedName = StripDomain(userName);

            //Create a search by User Name
            using var search = new DirectorySearcher(new DirectoryEntry(domain), $"(samaccountname={strippedName})");

            //Get the group membership for the user
            search.PropertiesToLoad.Add(memberOf);

            var result = search.FindOne();
            if (result is not null)
            {
                int groupsCount = result.Properties[memberOf].Count;

                for (int counter = 0; counter < groupsCount; counter++)
                {
                    string groupName = GetGroup((string) result.Properties[memberOf][counter]);

                    if (groupName is not null)
                    {
                        userGroups.Add(new UserGroup { GroupName = groupName });
                    }
                }
            }

            return userGroups;
        }

        return Domains.AsParallel().Select(x => GetUserGroups(x)).SelectMany(y => y);
    }

    public User GetUserInfo(string userName)
    {
        User GetUserInfo(string domain)
        {
            //Strip the Domain Name from userName if included
            string strippedName = StripDomain(userName);
            var user = new User();

            //Create a search by User Name
            using var search = new DirectorySearcher(new DirectoryEntry(domain), $"(samaccountname={strippedName})");

            //Get the group membership for the user
            search.PropertiesToLoad.Add("sAMAccountName");
            search.PropertiesToLoad.Add("displayName");
            search.PropertiesToLoad.Add("mail");

            var result = search.FindOne();
            if (result is not null)
            {
                user.UserName = result.Properties["sAMAccountName"][0].ToString() ?? string.Empty;
                user.DisplayName = result.Properties["displayName"].Count > 0 ? result.Properties["displayName"][0].ToString() : string.Empty;
                user.Email = result.Properties["mail"].Count > 0 ? result.Properties["mail"][0].ToString() : string.Empty;
            }

            return user;
        }

        return Domains
            .AsParallel()
            .Select(GetUserInfo)
            .FirstOrDefault(y => y.UserName != null);
    }

    public (bool, IEnumerable<string>) IsUserInGroups(string userName, IEnumerable<string> groups)
    {
        var userGroups = groups
            .Intersect(GetUserGroups(userName)
            .Select(x => x.GroupName));

        bool result = userGroups.Any();

        return (result, userGroups);
    }

    private static string ToLDAP(string value)
    {
        if (value.Contains("LDAP://"))
            return value;

        return $"LDAP://{value}";
    }

    private static string StripDomain(string userName)
    {
        //Strip the Domain Name from userName if included
        if (userName.Contains('\\'))
        {
            int i = userName.IndexOf('\\');
            userName = userName[(i + 1)..];
        }

        return userName;
    }

    private static User GetUser(string domain)
    {
        var user = new User();

        //Get the object by DN
        using var de = new DirectoryEntry(ToLDAP(domain));

        //Ignore other objects
        if (de.SchemaClassName == "user")
        {
            user.UserName = de.Properties["sAMAccountName"].Value?.ToString() ?? string.Empty;
            user.DisplayName = de.Properties["displayName"].Value?.ToString() ?? string.Empty;
            user.Email = de.Properties["mail"].Value?.ToString() ?? string.Empty;
        }

        return user;
    }

    private static string GetGroup(string domain)
    {
        //Get the object by DN
        using var de = new DirectoryEntry(ToLDAP(domain));

        //Ignore other objects
        if (de?.SchemaClassName == "group")
        {
            return de.Properties["name"].Value?.ToString();
        }

        return string.Empty;
    }

    public IEnumerable<User> GetGroupUsers(IEnumerable<string> groups)
        => groups
        .AsParallel()
        .Select(GetGroupUsers)
        .SelectMany(y => y);
}
