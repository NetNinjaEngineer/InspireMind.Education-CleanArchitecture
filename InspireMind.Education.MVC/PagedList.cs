namespace InspireMind.Education.MVC;

public class PagedList<T>
{
    public PagedList(int pageNumber, int pageSize, int totalCount, List<T> items, Product selectedProduct)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        SelectedProduct = selectedProduct;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public Product SelectedProduct { get; set; }
}
