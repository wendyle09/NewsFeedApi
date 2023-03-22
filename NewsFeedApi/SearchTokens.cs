namespace NewsFeedApi
{
	public class SearchTokens
	{
		private readonly string[] _stopWords = {"a", "an", "but", "for", "in", "it", "my", "or", "our", "the", "their", "to", "you", "your"};
        private string[] _tokens;

		public string[] Filtered;

        public SearchTokens(string search)
		{
			_tokens = search.Split("+");
            Filtered = FilterOutStopWords();
        }

		private string[] FilterOutStopWords ()
		{
			return _tokens.Except(_stopWords).ToArray();
		}
	}
}

