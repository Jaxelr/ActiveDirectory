using ActiveDirectory.Models.Internal;
using ActiveDirectory.Models.Operations;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetIsUserInGroup : RouteMetaData
    {
        private const string TagInfo = "User";
        private const string DescriptionInfo = "Returns a boolean indicating if if belongs to one of the groups, plus an array of belonged groups";

        public override string Description => DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"An object of type {nameof(IsUserInGroupResponse)}",
                Response = typeof(IsUserInGroupResponse)
            },
            new RouteMetaDataResponse
            {
                Code = 500,
                Description = "A response if an internal server error is detected.",
                Response = typeof(FailedResponse),
            }
        };

        public override string Tag => TagInfo;

        public override string OperationId => nameof(GetIsUserInGroup);
    }
}
