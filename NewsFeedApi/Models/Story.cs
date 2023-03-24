using System.Text.RegularExpressions;

namespace NewsFeedApi.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Title { get; set; } = String.Empty;

        public int Time { get; set; }

        public string Url { get; set; } = String.Empty;

        public Boolean HasSearchTerms(string[] searchTerms)
        {
            if (searchTerms.Length == 1 && searchTerms.First() == "")
            {
                return true;
            }

            string titleLowerCase = Title.ToLower();

            foreach (string term in searchTerms)
            {
                string pattern = @"(^|[-\s])" + term.ToLower() + @"(es|s)?([-'’:\s]|$)";

                if (Regex.IsMatch(titleLowerCase, pattern))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

