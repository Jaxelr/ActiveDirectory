using System.Collections.Generic;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetUserGroups : RouteMetaData
    {
        const string DescriptionInfo = "Returns a user domain groups";
        const string TagInfo = "User";

        public override string Description { get; } = DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
        new RouteMetaDataResponse
        {
            Code = 200,
            Description = $"A list of {nameof(UserGroup)}s",
            Response = typeof(IEnumerable<UserGroup>)
        }
    };

        public override string Tag { get; } = TagInfo;

        public override string OperationId { get; } = nameof(GetUserGroups);
    }
}
