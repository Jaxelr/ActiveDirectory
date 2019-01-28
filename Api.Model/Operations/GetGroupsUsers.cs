using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Get Groups Users")]
    [Route("/GetGroupsUsers", Verbs = "GET POST PUT", Summary = "Get the AD Users of the selected groups", Notes = "Get the AD Users of the selected groups")]
    [DataContract(Namespace = Default.Namespace)]
    public class GetGroupsUsers : IReturn<GetGroupUsersResponse>
    {
        [DataMember]
        [ApiMember(Name = "Groups", ParameterType = "path", DataType = "string", IsRequired = true, Description = "Groups to search", AllowMultiple = false)]
        public IEnumerable<string> Groups { get; set; }
    }

    //Reusing Response poco GetGroupUsersResponse
}