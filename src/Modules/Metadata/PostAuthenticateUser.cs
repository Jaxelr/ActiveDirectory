using ActiveDirectory.Entities.Operations;
using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class PostAuthenticateUser : RouteMetaData
    {
        private const string TagInfo = "User";
        private const string DescriptionInfo = "Returns an authenticated user";

        public override string Description => DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"An object of type {nameof(User)}",
                Response = typeof(User)
            },
        };

        public override string Tag => TagInfo;

        public override RouteMetaDataRequest[] Requests { get; } =
        {
            new RouteMetaDataRequest
            {
                Request = typeof(AuthenticUserRequest)
            },
        };

        public override string OperationId => nameof(PostAuthenticateUser);
    }
}
