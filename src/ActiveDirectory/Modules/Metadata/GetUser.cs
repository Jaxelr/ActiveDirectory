using ActiveDirectory.Models.Entities;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetUser : RouteMetaData
    {
        private const string DescriptionInfo = "Returns a user";
        private const string TagInfo = "User";

        public override string Description => DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"An object of type {nameof(User)}",
                Response = typeof(User)
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

        public override string Tag => TagInfo;

        public override string OperationId => nameof(GetUser);
    }
}
