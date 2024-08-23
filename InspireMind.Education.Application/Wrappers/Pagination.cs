namespace InspireMind.Education.Application.Wrappers;
public class Pagination<T>
{
    public IEnumerable<T> Data { get; set; }
    public PaginationMetaData MetaData { get; set; }

    public Pagination(int pageNumber, int pageSize, int count, IEnumerable<T> data)
    {
        Data = data;
        MetaData = new()
        {
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = count,
            TotalPages = (int)Math.Ceiling(count / (double)pageSize)
        };
    }

    public static Pagination<T> ToPaginatedResult(int pageNumber, int pageSize, int count, IEnumerable<T> data)
        => new(pageNumber, pageSize, count, data);
}
