using receiptor.NET.DTOs;
using receiptor.NET.Models;

namespace receiptor.NET.Interfaces;

public interface IReceiptRepository
{
    Task<List<Receipt>> GetAllReceiptsAsync();
    Task<Receipt?> GetReceiptByIdAsync(int id);
    Task<Receipt> CreateReceiptAsync(Receipt receipt);
    Task<Receipt?> UpdateReceiptAsync(int id, UpdateReceiptRequestDto updateReceiptRequestDto);
    Task<Receipt?> DeleteReceiptAsync(int id);
}