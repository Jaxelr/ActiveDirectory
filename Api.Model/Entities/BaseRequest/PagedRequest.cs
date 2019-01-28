using Api.Model.ActiveDirectory.Properties;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Entities
{
    [DataContract(Namespace = Default.Namespace)]
    public class PagedRequest
    {
        [DataMember(Order = 1)]
        public int Page { get; set; }

        [DataMember(Order = 2)]
        public int PageSize { get; set; }
    }
}