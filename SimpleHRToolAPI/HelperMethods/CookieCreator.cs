

namespace SimpleHRToolAPI.HelperMethods
{
    internal static class CookieCreator
    {
        public static void CreateCookie(string tokenString, DateTime expirationTime,  HttpResponse httpResponse)
        {
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = expirationTime;
            cookieOptions.Path = "/SimpleHRTool";
            cookieOptions.Secure = true;
            httpResponse.Cookies.Append("SimpleHRTool", tokenString, cookieOptions);
        }
    }
}
