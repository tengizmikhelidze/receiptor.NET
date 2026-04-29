using receiptor.NET.Models;
using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class ReceiptDTO
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public QuantityUnit QuantityUnit { get; set; }

    public decimal Quantity { get; set; }

    public decimal AveragePrice { get; set; }
}