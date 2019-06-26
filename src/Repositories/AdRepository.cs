using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;

namespace ActiveDirectory
{
    public class AdRepository : IAdRepository
    {
        public IEnumerable<string> Domains;

        public AdRepository()
        {
            Domains = new List<string> { ToLDAP(Domain.GetCurrentDomain().Name) };
        }

        public AdRepository(IEnumerable<string> domains) 
        {
            Domains = domains.Select(x => ToLDAP(x));
        }

        public (bool, string) AuthenticateUser(string userName, string password) => AuthenticateUser(userName, password, Domains.First());

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

                var search = new DirectorySearcher(de);
                var result = search.FindOne();

                if (result is SearchResult)
                {
                    return (true, string.Empty);
                }
            }
            catch (Exception x)
            {
                return (false, x.Message);
            }

            return (false, "User not Found.");
        }

        public IEnumerable<User> GetGroupUsers(string group)
        {
            IEnumerable<User> GetGroupUsers(string domain)
            {
                string member = @"member";
                var response = new List<User>();

                //Create a search by Group Name
                var search = new DirectorySearcher(new DirectoryEntry(domain), $"(cn={group})");

                //Get the users in the group
                search.PropertiesToLoad.Add(member);

                var result = search.FindOne();
                if (result is SearchResult)
                {
                    int usersCount = result.Properties[member].Count;

                    for (int counter = 0; counter < usersCount; counter++)
                    {
                        var user = GetUser(result.Properties[member][counter].ToString());

                        if (user is User)
                        {
                            user.Group = group;
                            response.Add(user);
                        }
                    }
                }

                return response;
            }

            return Domains.AsParallel().Select(x => GetGroupUsers(x)).SelectMany(y => y).ToList();
        }

        public IEnumerable<UserGroup> GetUserGroups(string userName)
        {
            IEnumerable<UserGroup> GetUserGroups(string domain)
            {
                string memberOf = @"memberOf";
                var userGroups = new List<UserGroup>();
                //Strip the Domain Name from userName if included
                string strippedName = StripDomain(userName);

                //Create a search by User Name
                var search = new DirectorySearcher(new DirectoryEntry(domain), $"(samaccountname={strippedName})");

                //Get the group membership for the user
                search.PropertiesToLoad.Add(memberOf);

                var result = search.FindOne();
                if (result is SearchResult)
                {
                    int groupsCount = result.Properties[memberOf].Count;

                    for (int counter = 0; counter < groupsCount; counter++)
                    {
                        string groupName = GetGroup((string) result.Properties[memberOf][counter]);

                        if (groupName is string)
                        {
                            userGroups.Add(new UserGroup { GroupName = groupName });
                        }
                    }
                }

                return userGroups;
            }

            return Domains.AsParallel().Select(x => GetUserGroups(x)).SelectMany(y => y).ToList();
        }

        public User GetUserInfo(string userName)
        {
            User GetUserInfo(string domain)
            {
                //Strip the Domain Name from userName if included
                string strippedName = StripDomain(userName);
                var user = new User();

                //Create a search by User Name
                var search = new DirectorySearcher(new DirectoryEntry(domain), $"(samaccountname={strippedName})");

                //Get the group membership for the user
                search.PropertiesToLoad.Add("sAMAccountName");
                search.PropertiesToLoad.Add("displayName");
                search.PropertiesToLoad.Add("mail");

                var result = search.FindOne();
                if (result is SearchResult)
                {
                    user.UserName = result.Properties["sAMAccountName"][0].ToString() ?? string.Empty;
                    user.DisplayName = result.Properties["displayName"].Count > 0 ? result.Properties["displayName"][0].ToString() : string.Empty;
                    user.Email = result.Properties["mail"].Count > 0 ? result.Properties["mail"][0].ToString() : string.Empty;
                }

                return user;
            }

            return Domains
                .AsParallel()
                .Select(x => GetUserInfo(x))
                .FirstOrDefault(y => y.UserName != null);
        }

        public bool IsUserInGroups(string userName, IEnumerable<string> groups)
        {
            var userGroups = groups
                .Intersect(GetUserGroups(userName)
                .Select(x => x.GroupName));

            return userGroups.Count() > 0 ? true : false;
        }

        private string ToLDAP(string value)
        {
            if (value.Contains("LDAP://"))
                return value;

            return $"LDAP://{value}";
        }

        private string StripDomain(string userName)
        {
            //Strip the Domain Name from userName if included
            if (userName.Contains("\\"))
            {
                int i = userName.IndexOf("\\");
                userName = userName.Substring(i + 1, (userName.Length - i) - 1);
            }

            return userName;
        }

        private User GetUser(string domain)
        {
            var user = new User();

            //Get the object by DN
            var de = new DirectoryEntry(ToLDAP(domain));

            //Ignore other objects
            if (de.SchemaClassName == "user")
            {
                user.UserName = de.Properties["sAMAccountName"].Value?.ToString() ?? string.Empty;
                user.DisplayName = de.Properties["displayName"].Value?.ToString() ?? string.Empty;
                user.Email = de.Properties["mail"].Value?.ToString() ?? string.Empty;
            }

            return user;
        }

        private string GetGroup(string domain)
        {
            //Get the object by DN
            var de = new DirectoryEntry(ToLDAP(domain));

            //Ignore other objects
            if (de.SchemaClassName == "group")
            {
                return de.Properties["name"].Value?.ToString();
            }

            return string.Empty;
        }

        public IEnumerable<User> GetGroupUsers(IEnumerable<string> groups)
            => groups
            .AsParallel()
            .Select(x => GetGroupUsers(x))
            .SelectMany(y => y)
            .ToList();
    }
}
