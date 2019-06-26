using System.Collections.Generic;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetGroupUsers : RouteMetaData
    {
        private const string TagInfo = "Group User";
        private const string DescriptionInfo = "Returns a user by group";

        public override string Description { get; } = DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"A list of {nameof(User)}s",
                Response = typeof(IEnumerable<User>)
            }
        };

        public override string Tag { get; } = TagInfo;

        public override string OperationId => nameof(GetGroupUsers);
    }
}
