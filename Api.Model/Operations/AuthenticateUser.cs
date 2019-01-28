using Api.Model.ActiveDirectory.Entities;
using Api.Model.ActiveDirectory.Properties;
using ServiceStack.ServiceHost;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Operations
{
    [Api("Validate if the User is Valid")]
    [Route("/AuthenticateUser/{UserName}", Verbs = "GET POST PUT", Summary = "Validate if the user is valid on the Domain", Notes = "Validate if the user is valid on the Domain")]
    [DataContract(Namespace = Default.Namespace)]
    public class AuthenticateUser : IReturn<AuthenticateUserResponse>
    {
        [DataMember]
        [ApiMember(Name = "UserName", ParameterType = "path", DataType = "string", IsRequired = true, Description = "UserName", AllowMultiple = false)]
        public string UserName { get; set; }

        [DataMember]
        [ApiMember(Name = "Password", ParameterType = "body", DataType = "string", IsRequired = true, Description = "Password", AllowMultiple = false)]
        public string Password { get; set; }

        [DataMember]
        [ApiMember(Name = "Domain", ParameterType = "body", DataType = "string", IsRequired = false, Description = "Domain", AllowMultiple = false)]
        public string Domain { get; set; }
    }

    [DataContract(Namespace = Default.Namespace)]
    public class AuthenticateUserResponse : BaseResponse
    {
        [DataMember]
        public bool IsUserValid { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}