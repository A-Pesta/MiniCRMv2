namespace MiniCRMv2.Models;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int TotalPages
    {
        get
        {
            return (int)Math.Ceiling((double)TotalCount / PageSize);
        }
    }

    public bool HasPreviousPage
    {
        get
        {
            return PageNumber > 1;

        }
    }

    public bool HasNextPage
    {
        get
        {
            return PageNumber < TotalPages;
        }
    }
}