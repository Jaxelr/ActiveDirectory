using Api.Helpers;
using Api.Model.ActiveDirectory.Operations;
using Repoes;
using ServiceStack.Common;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceInterface;
using System.Diagnostics;
using System.Linq;

namespace Api.ServiceInterface
{
    public class AdService : Service
    {
        private readonly IAdRepository repo;
        private readonly Stopwatch watch;
        private System.TimeSpan cacheSpan;

        public AdService(IAdRepository repo, Stopwatch watch, System.TimeSpan cacheSpan)
        {
            this.repo = repo;
            this.watch = watch;
            this.cacheSpan = cacheSpan;
            this.watch.Restart();
        }

        public object Any(GetUserGroups request)
        {
            try
            {
                string cachedKey = UrnId.Create<GetUserGroups>(request.UserName);
                return RequestContext.CachedContext(Cache, cachedKey, cacheSpan, () =>
                {
                    var result = repo.GetUserGroups(request.UserName);
                    watch.Stop();

                    return new GetUserGroupsResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = watch.ElapsedMilliseconds,
                        Records = result.Count()
                    };
                });
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }

        public object Any(GetUser request)
        {
            try
            {
                string cachedKey = UrnId.Create<GetUser>(request.UserName);
                return RequestContext.CachedContext(Cache, cachedKey, cacheSpan, () =>
                {
                    var result = repo.GetUserInfo(request.UserName);
                    watch.Stop();

                    return new GetUserResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = watch.ElapsedMilliseconds,
                        Records = result != null ? 1 : 0
                    };
                });
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }

        public object Any(GetGroupUsers request)
        {
            try
            {
                string cachedKey = UrnId.Create<GetGroupUsers>(request.Group);
                return RequestContext.CachedContext(Cache, cachedKey, cacheSpan, () =>
                {
                    var result = repo.GetGroupUsers(request.Group);
                    watch.Stop();

                    return new GetGroupUsersResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = watch.ElapsedMilliseconds,
                        Records = result.Count()
                    };
                });
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }

        public object Any(GetGroupsUsers request)
        {
            try
            {
                string cachedKey = UrnId.Create<GetGroupsUsers>(request.Groups.Aggregate((i, j) => i + j));
                return RequestContext.CachedContext(Cache, cachedKey, cacheSpan, () =>
                {
                    var result = repo.GetGroupUsers(request.Groups);
                    watch.Stop();

                    return new GetGroupUsersResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = watch.ElapsedMilliseconds,
                        Records = result.Count()
                    };
                });
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }

        public object Any(IsUserInGroups request)
        {
            try
            {
                string cachedKey = UrnId.Create<IsUserInGroups>(request.UserName, request.Groups.Aggregate((i, j) => i + j));
                return RequestContext.CachedContext(Cache, cachedKey, cacheSpan, () =>
                {
                    bool result = repo.IsUserInGroups(request.UserName, request.Groups);
                    watch.Stop();

                    return new IsUserInGroupsResponse()
                    {
                        IsUserInGroup = result,
                        ElapsedMiliseconds = watch.ElapsedMilliseconds,
                        Records = 1
                    };
                });
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }

        public object Any(AuthenticateUser request)
        {
            try
            {
                var result = repo.AuthenticateUser(request.UserName, request.Password, request.Domain);
                watch.Stop();

                return new AuthenticateUserResponse()
                {
                    IsUserValid = result.Item1,
                    Message = result.Item2,
                    ElapsedMiliseconds = watch.ElapsedMilliseconds,
                    Records = 1
                };
            }
            catch (WebServiceException e)
            {
                throw e;
            }
        }
    }
}
