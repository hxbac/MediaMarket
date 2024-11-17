﻿namespace MediaMarket.Domain.Common
{
    public static class Router
    {
        public const string singleRoute = "/{id}";
        public const string slug = "/{slug}";
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
                public const string GetProfile = Prefix + "/get-profile";
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
                public const string Show = Prefix + slug;
                public const string GetProductsHomePage = Prefix + "/get-list-by-category";
                public const string Create = Prefix;
                public const string MyProducts = Prefix + "/my-products";
            }
        }

        public static class CategoryRouting
        {
            public const string Prefix = rule + "categories";
            public static class Action
            {
                public const string Index = Prefix;
                public const string GetAll = Prefix + "/get-all";
            }
        }

        public static class FileRouting
        {
            public const string Prefix = rule + "files";
            public static class Action
            {
                public const string UploadSingle = Prefix + "/upload";
            }
        }

        public static class OrderRouting
        {
            public const string Prefix = rule + "orders";
            public const string Callback = Prefix + "/callback";
            public static class Action
            {
                public const string Create = Prefix + "/create";
                public const string StripeCallback = Callback + "/stripe";
            }
        }
    }
}
