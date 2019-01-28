using Api.Model.ActiveDirectory.Entities;
using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Get User")]
    [Route("/GetUser/{UserName}", Verbs = "GET POST PUT", Summary = "Get the User", Notes = "Get the User")]
    [DataContract(Namespace = Default.Namespace)]
    public class GetUser : IReturn<GetUserGroupsResponse>
    {
        [DataMember]
        [ApiMember(Name = "UserName", ParameterType = "path", DataType = "string", IsRequired = true, Description = "UserName", AllowMultiple = false)]
        public string UserName { get; set; }
    }

    [DataContract(Namespace = Default.Namespace)]
    public class GetUserResponse : BaseResponse
    {
        [DataMember]
        public User Result { get; set; }
    }
}