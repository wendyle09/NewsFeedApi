using NewsFeedApi.Pagination;

namespace NewsFeedApi.Helpers
{
	public class ResponseHelper
	{
		public static Object GetMetadata<T>(PagedList<T> pagedList, int currentPage, int pageSize, string queryPath)
        {
            return new
            {
                pagedList.CurrentPage,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages,
            };
        }
    }
}

