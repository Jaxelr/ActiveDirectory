using Api.Model.ActiveDirectory.Properties;
using System.Runtime.Serialization;

namespace Api.Model.ActiveDirectory.Entities
{
    [DataContract(Namespace = Default.Namespace)]
    public class PagedResponse : BaseResponse
    {
        [DataMember(Order = 1)]
        public int? CurrentPage { get; set; }

        [DataMember(Order = 2)]
        public int? TotalPages { get; set; }

        [DataMember(Order = 3)]
        public int? TotalRecords { get; set; }
    }
}