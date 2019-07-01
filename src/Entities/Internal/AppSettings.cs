using System.Collections.Generic;

namespace ActiveDirectory
{
    public class AppSettings
    {
        public CacheConfig Cache { get; set; }
        public ICollection<string> Domains { get; set; }
        public ICollection<string> Addresses { get; set; }
    }
}
