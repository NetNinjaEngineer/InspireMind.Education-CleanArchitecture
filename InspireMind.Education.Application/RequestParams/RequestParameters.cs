namespace InspireMind.Education.Application.RequestParams;
public abstract class RequestParameters
{
    #region Fields
    private const int _maxPageSize = 50;
    private int _pageSize = 5;
    private string? searchTerm;
    #endregion

    #region Properities
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > _maxPageSize ? _pageSize : value; }
    }


    public string? SearchTerm
    {
        get { return searchTerm; }
        set { searchTerm = value!.ToLower(); }
    }
    #endregion
}
