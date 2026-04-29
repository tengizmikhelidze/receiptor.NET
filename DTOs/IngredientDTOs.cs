using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class IngredientDTO
{
    public int Id { get; set; }
    public int ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}

public class CreateIngredientRequestDTO
{
    public int ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}

public class UpdateIngredientRequestDTO
{
    public int ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}