using receiptor.NET.DTOs;
using receiptor.NET.Models;

namespace receiptor.NET.Mappers;

public static class ReceiptMapper
{
    public static ReceiptDTOs ToReceiptDto(this Receipt receipt)
    {
        return new ReceiptDTOs
        {
            Id = receipt.Id,
            CategoryId = receipt.CategoryId,
            Name = receipt.Name,
            Description = receipt.Description,
            CreatedAt = receipt.CreatedAt,
            ModifiedAt = receipt.ModifiedAt,
            QuantityUnit = receipt.QuantityUnit,
            Quantity = receipt.Quantity,
            AveragePrice = receipt.AveragePrice,
            Ingredients = receipt.Ingredients
        };
    }

    public static Receipt toReceiptFromCreateDTO(this CreateReceiptRequestDto createReceiptRequestDto)
    {
        return new Receipt
        {
            CategoryId = createReceiptRequestDto.CategoryId,
            Name = createReceiptRequestDto.Name,
            Description = createReceiptRequestDto.Description,
            Ingredients = createReceiptRequestDto.Ingredients
        };
    }
}