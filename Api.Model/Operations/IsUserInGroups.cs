using Api.Model.ActiveDirectory.Entities;
using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Validate if the User is in a Group")]
    [Route("/IsUserInGroups/", Verbs = "GET POST PUT", Summary = "Validate if the user belongs to the group given", Notes = "Validate if the user belongs to the group given")]
    [DataContract(Namespace = Default.Namespace)]
    public class IsUserInGroups : IReturn<IsUserInGroupsResponse>
    {
        [DataMember]
        [ApiMember(Name = "UserName", ParameterType = "body", DataType = "string", IsRequired = true, Description = "Username ", AllowMultiple = false)]
        public string UserName { get; set; }

        [DataMember]
        [ApiMember(Name = "Groups", ParameterType = "body", DataType = "string", IsRequired = true, Description = "Group array to search ", AllowMultiple = false)]
        public IEnumerable<string> Groups { get; set; }
    }

    [DataContract(Namespace = Default.Namespace)]
    public class IsUserInGroupsResponse : BaseResponse
    {
        [DataMember]
        public bool IsUserInGroup { get; set; }
    }
}