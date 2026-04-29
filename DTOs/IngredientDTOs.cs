using System.ComponentModel.DataAnnotations;
using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class IngredientBase
{
    [Required]
    [MinLength(1, ErrorMessage = "The {0} must be at least {1} characters long.")]
    [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}

public class IngredientDTO: IngredientBase
{
    public int Id { get; set; }
    public int? ReceiptId { get; set; }
}

public class CreateIngredientRequestDTO: IngredientBase
{
}

public class UpdateIngredientRequestDTO
{
    public int? ReceiptId { get; set; }
    public string Name { get; set; } = string.Empty;
        
    public decimal Quantity { get; set; }
    public QuantityUnit QuantityUnit { get; set; }
}