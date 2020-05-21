using System.Collections.Generic;

namespace ActiveDirectory.Entities
{
    public class AppSettings
    {
        public CacheConfig Cache { get; set; }
        public RouteDefinition RouteDefinition { get; set; }
        public ICollection<string> Domains { get; set; }
        public ICollection<string> Addresses { get; set; }
    }
}
