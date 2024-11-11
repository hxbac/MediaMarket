using System.Text.RegularExpressions;

namespace MediaMarket.Application.Utils
{
    public static class Helper
    {
        public static string StringToSlug(string input)
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
