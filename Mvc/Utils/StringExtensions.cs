namespace Mvc.Utils;

public static class StringExtensions
{
    //寫一個string to slug
    public static string ToSlug(this string str)
    {
        return str.ToLower().Replace(" ", "-");
    }
}