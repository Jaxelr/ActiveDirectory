using System.Collections.Generic;
using ActiveDirectory.Models.Entities;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetGroupUsers : RouteMetaData
    {
        private const string TagInfo = "Group User";
        private const string DescriptionInfo = "Returns a user by group";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"A list of {nameof(User)}s",
                Response = typeof(IEnumerable<User>)
            },
            new RouteMetaDataResponse
            {
                Code = 204,
                Description = $"An empty result pertaining to a not found {nameof(User)}"
            },
            new RouteMetaDataResponse
            {
                Code = 500,
                Description = "A response if an internal server error is detected.",
                Response = typeof(FailedResponse),
            }
        };

        public override string Description => DescriptionInfo;

        public override string Tag => TagInfo;

        public override string OperationId => nameof(GetGroupUsers);
    }
}
