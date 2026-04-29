using receiptor.NET.DTOs;
using receiptor.NET.Models;

namespace receiptor.NET.Mappers;

public static class ReceiptMapper
{
    public static ReceiptDTO ToReceiptDto(this Receipt receipt)
    {
        return new ReceiptDTO
        {
            Id = receipt.Id,
            CategoryId = receipt.CategoryId,
            Name = receipt.Name,
            Description = receipt.Description,
            CreatedAt = receipt.CreatedAt,
            ModifiedAt = receipt.ModifiedAt,
            QuantityUnit = receipt.QuantityUnit,
            Quantity = receipt.Quantity,
            AveragePrice = receipt.AveragePrice
        };
    }
}