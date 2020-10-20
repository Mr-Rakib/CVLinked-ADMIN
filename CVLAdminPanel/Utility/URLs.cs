namespace CVLAdminPanel.Utility
{
    public static class URLs
    {
        public static string API = ConfigureAppSettings.Configure.GetSection("ApiUrl").Value;
        public static string Database = ConfigureAppSettings.Configure.GetSection("Database")["ConnectionString"];
    }
}
