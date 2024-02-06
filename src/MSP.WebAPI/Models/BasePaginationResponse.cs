namespace MSP.WebAPI.Models;

public class BasePaginationResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int DataCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public BasePaginationResponse(IEnumerable<T> data, int skip, int take, int dataCount)
    {
        Data = data;
        DataCount = dataCount;
        PageNumber = dataCount == 0 || skip == 0 || take == 0 ? 1 : (skip / take) + 1;
        PageSize = Data.Count();
        TotalPages = (int)Math.Ceiling((double)dataCount / take);
    }
}