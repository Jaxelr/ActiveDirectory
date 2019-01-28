using Api.Model.ActiveDirectory.Properties;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Entities
{
    [DataContract(Namespace = Default.Namespace)]
    public class User
    {
        [DataMember(Order = 1)]
        public string UserName { get; set; }

        [DataMember(Order = 2)]
        public string DisplayName { get; set; }

        [DataMember(Order = 3)]
        public string Email { get; set; }

        [DataMember(Order = 4)]
        public string Group { get; set; }
    }
}