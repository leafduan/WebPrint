namespace WebPrint.Web.Mvc.Helper
{
    public class JavaScriptSupport
    {
        public static string EscapeSpecialCharacters(string input)
        {
            input = input.Replace(@"\", @"\\"); // Escape backslash
            input = input.Replace(@"'", @"\'"); // Escape apostrophes
            input = input.Replace("\"", "\\\""); // Escape double-quotes

            return input;
        }
    }
}