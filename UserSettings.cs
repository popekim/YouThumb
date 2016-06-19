namespace YouThumb
{
    public static class UserSettings
    {
        public static readonly string FONTNAME = "fontname";
        public static readonly string CLIENT_ID = "clientID";
        public static readonly string CLIENT_SECRET = "clientSecret";

        public static string Get(string key)
        {
            var value = Properties.Settings.Default[key];
            return (value == null) ? "" : value.ToString();
        }
    }
}
