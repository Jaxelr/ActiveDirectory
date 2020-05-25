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
            }
        };

        public override string Tag => TagInfo;

        public override string OperationId => nameof(GetIsUserInGroup);
    }
}
