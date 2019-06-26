using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetUser : RouteMetaData
    {
        private const string DescriptionInfo = "Returns a user";
        private const string TagInfo = "User";

        public override string Description { get; } = DescriptionInfo;

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"An object of type {nameof(User)}",
                Response = typeof(User)
            }
        };

        public override string Tag { get; } = TagInfo;

        public override string OperationId { get; } = nameof(GetUser);
    }
}
