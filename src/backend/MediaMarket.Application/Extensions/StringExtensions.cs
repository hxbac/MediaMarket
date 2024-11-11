using System.Text.RegularExpressions;

namespace MediaMarket.Application.Extensions
{
    public static class StringExtensions
    {
        private static Random _random = new Random();

        public static string GenerateRandomString(this string _, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        public static string ToSlug(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string slug = input.ToLower();

            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            slug = Regex.Replace(slug, @"-+", "-");

            return slug;
        }
    }
}
