using NewsFeedApi.Pagination;
using NewsFeedApi.Response;

namespace NewsFeedApi.Helpers
{
	public class ResponseHelper
	{
		public static Metadata GetMetadata<T>(PagedList<T> pagedList, int currentPage, int pageSize)
        {
            return new Metadata(
                pagedList.CurrentPage,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages
            );
        }
    }
}

