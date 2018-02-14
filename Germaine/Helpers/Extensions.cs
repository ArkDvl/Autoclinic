using Microsoft.AspNetCore.Http;

namespace Germaine.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        // public static int PasswordReset(this int val)
        // {
        //     //var age = DateTime.Today.Year - theDateTime.Year;
        //     if (val < 1)
        //         return false;

        //     return true;
        // }
    }
}