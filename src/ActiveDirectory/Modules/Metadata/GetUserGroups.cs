using System.Collections.Generic;
using ActiveDirectory.Models.Entities;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    //public class GetUserGroups : RouteMetaData
    //{
    //    private const string DescriptionInfo = "Returns the groups of a user on the domain";
    //    private const string TagInfo = "User";

    //    public override string Description => DescriptionInfo;

    //    public override RouteMetaDataResponse[] Responses { get; } =
    //    {
    //        new RouteMetaDataResponse
    //        {
    //            Code = 200,
    //            Description = $"A list of {nameof(UserGroup)}s",
    //            Response = typeof(IEnumerable<UserGroup>)
    //        },
    //        new RouteMetaDataResponse
    //        {
    //            Code = 204,
    //            Description = $"An empty result pertaining to a not found {nameof(UserGroup)}"
    //        },
    //        new RouteMetaDataResponse
    //        {
    //            Code = 500,
    //            Description = "A response if an internal server error is detected.",
    //            Response = typeof(FailedResponse),
    //        }
    //};

    //    public override string Tag => TagInfo;

    //    public override string OperationId => nameof(GetUserGroups);
    //}
}
