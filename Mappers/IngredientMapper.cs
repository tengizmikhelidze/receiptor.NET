using receiptor.NET.DTOs;
using receiptor.NET.Models;

namespace receiptor.NET.Mappers;

public static class IngredientMapper
{
    public static IngredientDTO ToIngredientDTO(this Ingredient ingredient)
    {
        return new IngredientDTO
        {
            Id = ingredient.Id,
            ReceiptId = ingredient.ReceiptId,
            Name = ingredient.Name,
            Quantity = ingredient.Quantity,
            QuantityUnit = ingredient.QuantityUnit
        };
    }
    
    public static Ingredient toIngredientFromCreateDTO(this CreateIngredientRequestDTO createIngredientRequestDTO)
    {
        return new Ingredient
        {
            ReceiptId = createIngredientRequestDTO.ReceiptId,
            Name = createIngredientRequestDTO.Name,
            Quantity = createIngredientRequestDTO.Quantity,
            QuantityUnit = createIngredientRequestDTO.QuantityUnit
        };
    }
}