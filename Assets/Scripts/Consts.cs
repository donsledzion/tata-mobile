
public static class Consts
{
#if UNITY_EDITOR
    public static readonly string BaseAPI_URL = "http://127.0.0.1:8000/api/";
    public static readonly string BaseURL = "http://127.0.0.1:8000/";
#elif PLATFORM_ANDROID
    public static readonly string BaseAPI_URL = "https://dzieciaki.ulinia8.pl/api/";
    public static readonly string BaseURL = "https://dzieciaki.ulinia8.pl/";
#endif


    public static readonly string TokenKey = "RememberedApiToken";

    internal static class Pictures
    {
        public static readonly string Storage = "storage";
    }

    internal static class URI
    {
        public static readonly string Login = "login";
        public static readonly string Logout = "logout";
        public static readonly string CheckToken = "check-token";
        public static readonly string Status = "status";
        public static readonly string FetchKids = "kids";
        public static readonly string FetchFeeds= "postsFeed";
    }
}
