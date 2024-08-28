namespace InspireMind.Education.Application.Wrappers;
public class Pagination<T>(int pageNumber, int pageSize, int count, IEnumerable<T> data)
{
    public IEnumerable<T> Data { get; set; } = data;
    public PaginationMetaData MetaData { get; set; } = new()
    {
        CurrentPage = pageNumber,
        PageSize = pageSize,
        TotalCount = count,
        TotalPages = (int)Math.Ceiling(count / (double)pageSize)
    };

    public static Pagination<T> ToPaginatedResult(int pageNumber, int pageSize, int count, IEnumerable<T> data)
        => new(pageNumber, pageSize, count, data);
}
