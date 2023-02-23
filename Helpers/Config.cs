namespace TaskSchedular.Helpers
{
    public class Config
    {
        public Config(IConfiguration configuration) {
            IConfig = configuration;
        }

        private static IConfiguration IConfig { get; set; }

        public static string Get(string key)
        {
            return IConfig.GetValue<string>(key);
        }

        public static string GetConnectionString(string key)
        {
            return IConfig.GetConnectionString(key);
        }

    }
}
