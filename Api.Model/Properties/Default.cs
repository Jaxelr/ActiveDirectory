namespace Api.Model.ActiveDirectory.Properties
{
    public static class Default
    {
        public const string Namespace = @"http://schemas.tsa.com/ActiveDirectory";
        public const string ServiceName = @"ActiveDirectory";

        public enum AppSettingKeys
        {
            CacheEnabled,
            CacheExpiry
        }
    }
}