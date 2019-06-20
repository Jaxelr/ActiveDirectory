namespace ActiveDirectory
{
    public class AppSettings
    {
        public Cache Cache { get; set; }
        public string[] Domains { get; set; }
    }

    public class Cache
    {
        public bool CacheEnabled { get; set; }
        public int CacheTimespan { get; set; }
        public int CacheMaxSize { get; set; }
    }
}
