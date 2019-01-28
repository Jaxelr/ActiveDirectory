using Api.Model.ActiveDirectory.Entities;
using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Get User Groups")]
    [Route("/GetUserGroups/{UserName}", Verbs = "GET POST PUT", Summary = "Get the AD groups of the User", Notes = "Get the AD groups of the User")]
    [DataContract(Namespace = Default.Namespace)]
    public class GetUserGroups : IReturn<GetUserGroupsResponse>
    {
        [DataMember]
        [ApiMember(Name = "UserName", ParameterType = "path", DataType = "string", IsRequired = true, Description = "Username to search", AllowMultiple = false)]
        public string UserName { get; set; }
    }

    [DataContract(Namespace = Default.Namespace)]
    public class GetUserGroupsResponse : BaseResponse
    {
        [DataMember]
        public IEnumerable<UserGroup> Result { get; set; }
    }
}