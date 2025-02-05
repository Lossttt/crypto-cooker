
namespace crypto_app.Config.API
{
    public static class ApiRoutes
    {
        private const string Root = "/api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public static class Users
        {
            public const string Index = Base + "/users";  
            public const string Authenticate = Index + "/authenticate";     
            public const string Register = Index + "/register"; 
        }
    }
}