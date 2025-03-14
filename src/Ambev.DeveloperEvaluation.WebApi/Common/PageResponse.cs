namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class PageResponse<T>
{
    public IEnumerable<T> Data { get; private set; }
    public int TotalItems { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }

    public PageResponse(List<T> items, int count, int pageNumber, int pageSize)
    {
        TotalItems = count;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Data = items;
    }
}