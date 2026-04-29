using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.Interfaces;
using receiptor.NET.Models;

namespace receiptor.NET.Repository;

public class ReceiptRepository: IReceiptRepository
{
    private readonly ApplicationDBContext _context;
    
    public ReceiptRepository(ApplicationDBContext context)
    {
        _context = context;
    }
    
    public Task<List<Receipt>> GetAllReceipts()
    {
        return _context.Receipts.ToListAsync();
    }

    public Task<Receipt?> GetReceiptById(int id)
    {
        return _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
    }
}