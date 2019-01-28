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
        private IAdRepository _repo;
        private Stopwatch _watch;
        private System.TimeSpan _cacheSpan;

        public AdService(IAdRepository repo, Stopwatch watch, System.TimeSpan cacheSpan)
        {
            _repo = repo;
            _watch = watch;
            _cacheSpan = cacheSpan;
            _watch.Restart();
        }

        public object Any(GetUserGroups request)
        {
            try
            {
                string cachedKey = UrnId.Create<GetUserGroups>(request.UserName);
                return RequestContext.CachedContext(Cache, cachedKey, _cacheSpan, () =>
                {
                    var result = _repo.GetUserGroups(request.UserName);
                    _watch.Stop();

                    return new GetUserGroupsResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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
                return RequestContext.CachedContext(Cache, cachedKey, _cacheSpan, () =>
                {
                    var result = _repo.GetUserInfo(request.UserName);
                    _watch.Stop();

                    return new GetUserResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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
                return RequestContext.CachedContext(Cache, cachedKey, _cacheSpan, () =>
                {
                    var result = _repo.GetGroupUsers(request.Group);
                    _watch.Stop();

                    return new GetGroupUsersResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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
                return RequestContext.CachedContext(Cache, cachedKey, _cacheSpan, () =>
                {
                    var result = _repo.GetGroupUsers(request.Groups);
                    _watch.Stop();

                    return new GetGroupUsersResponse()
                    {
                        Result = result,
                        ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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
                return RequestContext.CachedContext(Cache, cachedKey, _cacheSpan, () =>
                {
                    bool result = _repo.IsUserInGroups(request.UserName, request.Groups);
                    _watch.Stop();

                    return new IsUserInGroupsResponse()
                    {
                        IsUserInGroup = result,
                        ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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
                var result = _repo.AuthenticateUser(request.UserName, request.Password, request.Domain);
                _watch.Stop();

                return new AuthenticateUserResponse()
                {
                    IsUserValid = result.Item1,
                    Message = result.Item2,
                    ElapsedMiliseconds = _watch.ElapsedMilliseconds,
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