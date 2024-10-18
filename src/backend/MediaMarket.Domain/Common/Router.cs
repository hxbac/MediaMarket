namespace MediaMarket.Domain.Common
{
    public static class Router
    {
        public const string singleRoute = "/{id}";
        public const string root = "api";
        public const string version = "v1";
        public const string rule = root + "/" + version + "/";

        public static class AuthRouting
        {
            public const string Prefix = rule + "auth";

            public static class Action
            {
                public const string Login = Prefix + "/login";
                public const string Register = Prefix + "/register";
            }
        }
    }
}
