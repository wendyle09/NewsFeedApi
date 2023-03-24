using System;
namespace NewsFeedApi.Response
{
	public class Metadata
	{
        public readonly int CurrentPage;
        public readonly int PageSize;
        public readonly int TotalCount;
        public readonly int TotalPages;

        public Metadata(int currentPage, int pageSize, int totalCount, int totalPages)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
	}
}

