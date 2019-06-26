﻿using Carter.OpenApi;

namespace ActiveDirectory.Modules
{
    public class GetIsUserInGroup : RouteMetaData
    {
        private const string TagInfo = "User";
        private const string DescriptionInfo = "Returns an user that belongs to the group";

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

        public override string OperationId { get; } = nameof(GetIsUserInGroup);
    }
}
