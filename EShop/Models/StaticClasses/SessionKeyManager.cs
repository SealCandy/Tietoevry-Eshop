namespace EShop.Models.StaticClasses
{
    public static class SessionKeyManager
    {
        public static readonly string SessionKeyForUsers = "UserSession";
        public static string UserCookie;

        public static void SetUserCookieName(int id)
        {
             UserCookie = "Cart" + id;
        }
    }
}
