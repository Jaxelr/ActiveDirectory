using Carter.OpenApi;
using System.Collections.Generic;

namespace ActiveDirectory.Modules
{
    public class GetGroupUsers : RouteMetaData
    {
        public override string Description { get; } = "Returns a user by group";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse
            {
                Code = 200,
                Description = $"A list of {nameof(User)}s",
                Response = typeof(IEnumerable<User>)
            }
        };

        public override string Tag { get; } = "Group User";
    }
}
