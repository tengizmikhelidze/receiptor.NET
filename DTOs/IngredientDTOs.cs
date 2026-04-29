using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class IngredientDTOs
{
    public int ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}

public class CreateIngredientRequestDto
{
    public int ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}