using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetAuthenticateUser : RouteMetaData
    {
        public override string Description { get; } = "Returns an authenticated user";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"An object of type {nameof(User)}",
                Response = typeof(User)
            },
        };

        public override string Tag { get; } = "User";

        public override string OperationId { get; } = nameof(GetAuthenticateUser);
    }
}
