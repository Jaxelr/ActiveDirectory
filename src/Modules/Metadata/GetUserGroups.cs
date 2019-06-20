using System.Collections.Generic;
using Carter.OpenApi;

namespace AdSample.Modules
{
    public class GetUserGroups : RouteMetaData
    {
        public override string Description { get; } = "Returns a user domain groups";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
        new RouteMetaDataResponse
        {
            Code = 200,
            Description = $"A list of {nameof(UserGroup)}s",
            Response = typeof(IEnumerable<UserGroup>)
        }
    };

        public override string Tag { get; } = "User";

        public override string OperationId { get; } = nameof(GetUserGroups);
    }
}
