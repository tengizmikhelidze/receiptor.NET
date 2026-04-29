using System.ComponentModel.DataAnnotations;
using receiptor.NET.Models;
using receiptor.NET.Enums;

namespace receiptor.NET.DTOs;

public class ReceiptBase
{
    [Required]
    [MinLength(1, ErrorMessage = "The {0} must be at least {1} characters long.")]
    [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
    public string Description { get; set; } = string.Empty;
}

public class ReceiptDTOs: ReceiptBase
{
    public int Id { get; set; }
    public int? CategoryId { get; set; }

    public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    public QuantityUnit QuantityUnit { get; set; }

    public decimal Quantity { get; set; }

    public decimal AveragePrice { get; set; }
}

public class CreateReceiptRequestDto: ReceiptBase
{
    public int? CategoryId { get; set; }
    public List<CreateIngredientRequestDTO> Ingredients { get; set; } = new List<CreateIngredientRequestDTO>();
}

public class UpdateReceiptRequestDto: ReceiptBase
{
    public int? CategoryId { get; set; }
    public List<Ingredient>? Ingredients { get; set; }
}