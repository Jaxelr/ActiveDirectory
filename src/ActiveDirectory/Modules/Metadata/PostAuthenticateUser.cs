using System.Collections.Generic;
using ActiveDirectory.Models.Entities;
using ActiveDirectory.Models.Operations;
using Carter.OpenApi;
using FluentValidation.Results;

namespace ActiveDirectory.Modules
{
    //public class PostAuthenticateUser : RouteMetaData
    //{
    //    private const string TagInfo = "User";
    //    private const string DescriptionInfo = "Returns an authenticated user";

    //    public override string Description => DescriptionInfo;

    //    public override RouteMetaDataResponse[] Responses { get; } =
    //    {
    //        new RouteMetaDataResponse
    //        {
    //            Code = 200,
    //            Description = $"An object of type {nameof(User)}",
    //            Response = typeof(User)
    //        },
    //        new RouteMetaDataResponse
    //        {
    //            Code = 422,
    //            Description = "Validation errors from request",
    //            Response = typeof(IEnumerable<ValidationFailure>),
    //        },
    //        new RouteMetaDataResponse
    //        {
    //            Code = 500,
    //            Description = "A response if an internal server error is detected.",
    //            Response = typeof(FailedResponse),
    //        }
    //    };

    //    public override string Tag => TagInfo;

    //    public override RouteMetaDataRequest[] Requests { get; } =
    //    {
    //        new RouteMetaDataRequest
    //        {
    //            Request = typeof(AuthenticUserRequest)
    //        },
    //    };

    //    public override string OperationId => nameof(PostAuthenticateUser);
    //}
}
