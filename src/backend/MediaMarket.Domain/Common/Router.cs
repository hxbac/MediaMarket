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

        public static class TagRouting
        {
            public const string Prefix = rule + "tags";

            public static class Action
            {
                public const string Index = Prefix;
                public const string Create = Prefix;
                public const string Update = Prefix + singleRoute;
                public const string Delete = Prefix + singleRoute;
            }
        }

        public static class ProductRouting
        {
            public const string Prefix = rule + "products";
            public static class Action
            {
                public const string Show = Prefix + singleRoute;
            }
        }

        public static class CategoryRouting
        {
            public const string Prefix = rule + "categories";
            public static class Action
            {
                public const string Index = Prefix;
            }
        }
    }
}
