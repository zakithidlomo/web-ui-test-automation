using Microsoft.Extensions.Configuration;

namespace DemoWebShop.Automation.Support
{
    public static class ConfigReader
    {
        private static AppSettings? _settings;

        public static AppSettings GetSettings()
        {
            if (_settings != null) return _settings;

            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "appsettings.json");
            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            _settings = new AppSettings();
            config.Bind(_settings);
            return _settings;
        }

        public static TestSettings GetTestSettings() => GetSettings().TestSettings;
        public static BillingDetails GetBillingDetails() => GetSettings().BillingDetails;
    }
}
