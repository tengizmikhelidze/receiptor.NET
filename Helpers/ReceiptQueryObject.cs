using receiptor.NET.Enums;

namespace receiptor.NET.Helpers;

public class ReceiptQueryObject
{
    public string? Name { get; set; } = null;
    public string? Description { get; set; } = null;
    public List<ReceiptSort> Sort { get; set; } = new List<ReceiptSort>();
}

public class ReceiptSort
{
    public SortBy SortBy { get; set; } = SortBy.Desc;
    public ReceiptSortField Field { get; set; } = ReceiptSortField.CreatedAt;
}

public enum ReceiptSortField
{
    Name,
    Description,
    CreatedAt,
    ModifiedAt,
    Quantity
}