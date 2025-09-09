namespace LMS.Common
{
    public static class CommonUtility
    {
        public static int HttpStatusCode(int status)
        {
            // Map your service status codes to HTTP status codes
            return status switch
            {
                2 => 200,  // Success
                0 => 400,  // Bad request or failure
                404 => 404, // Not found
                _ => 500   // Default Internal Server Error
            };
        }
    }
}
