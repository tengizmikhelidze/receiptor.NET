using receiptor.NET.Models;

namespace receiptor.NET.Interfaces;

public interface IReceiptRepository
{
    Task<List<Receipt>> GetAllReceipts();
}