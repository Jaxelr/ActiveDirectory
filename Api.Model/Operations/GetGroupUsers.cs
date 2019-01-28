using Api.Model.ActiveDirectory.Entities;
using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Get Group Users")]
    [Route("/GetGroupUsers/{Group}", Verbs = "GET POST PUT", Summary = "Get the AD Users of the selected group", Notes = "Get the AD Users of the selected group")]
    [DataContract(Namespace = Default.Namespace)]
    public class GetGroupUsers : IReturn<GetGroupUsersResponse>
    {
        [DataMember]
        [ApiMember(Name = "Group", ParameterType = "path", DataType = "string", IsRequired = true, Description = "Group to search", AllowMultiple = false)]
        public string Group { get; set; }
    }

    [DataContract(Namespace = Default.Namespace)]
    public class GetGroupUsersResponse : BaseResponse
    {
        [DataMember]
        public IEnumerable<User> Result { get; set; }
    }
}