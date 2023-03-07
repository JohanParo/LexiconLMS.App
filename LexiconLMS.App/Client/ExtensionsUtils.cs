using System;

namespace LexiconLMS.App.Client
{
    public static class ExtensionsUtils
    {
        //public static string TrimString(this string text, int length = 300)
        //{
        //    if (text.Length > length)
        //    {
        //        return string.Concat(text.AsSpan(text.Length - (length - 3)), "...");
        //    }

        //    return text;
        //}
        public static string TrimString(this string value, int length = 300)
        {
            if (value == null || value.Length < length || value.IndexOf(" ", length) == -1)
                return value!;

            return string.Concat(value[..value.IndexOf(" ", length)],"...");
        }
    }


}
