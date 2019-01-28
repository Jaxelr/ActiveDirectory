using Api.Model.ActiveDirectory.Properties;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Entities
{
    [DataContract(Namespace = Default.Namespace)]
    public class UserGroup
    {
        [DataMember(Order = 1)]
        public string GroupName { get; set; }
    }
}