using System;
namespace NewsFeedApi.Helpers
{
	public class ResponseHelper
	{
		public static Object GetMetadata<T>(PagedList<T> pagedList, int currentPage, int pageSize, string queryPath)
        {
            var links = GetPaginationLinks<T>(pagedList, currentPage, pageSize, queryPath);

            var metadata = new
            {
                pagedList.CurrentPage,
                pagedList.HasNext,
                pagedList.HasPrevious,
                pagedList.PageSize,
                pagedList.TotalCount,
                pagedList.TotalPages,
                links
            };

            return metadata;
        }

        private static Object GetPaginationLinks<T>(PagedList<T> pagedList, int currentPage, int pageSize, string queryPath)
        {
            return new
            {
                self = $"/{queryPath}? page={currentPage}&pageSize={pageSize}",
                previous = pagedList.HasPrevious ? $"/{queryPath}?page={currentPage - 1}&pageSize={pageSize}" : null,
                next = pagedList.HasNext ? $"/{queryPath}?page={currentPage}&pageSize={pageSize}" : null,
                first = $"/{queryPath}?page=1&pageSize={pageSize}",
                last = $"/{queryPath}?page={pagedList.TotalPages}&pageSize={pageSize}"
            };
        }
    }
}

