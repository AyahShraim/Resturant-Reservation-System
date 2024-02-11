
namespace RestaurantReservation.Db.Pagination
{
    public class PaginationMetaData
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalRecords { get; init; }

        public PaginationMetaData(int totalRecords, int pageSize, int pageNumber)
        {
            PageNumber = pageNumber; 
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }
    }
}
