using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
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

    public async Task<List<Receipt>> GetAllReceiptsAsync()
    {
        return await _context.Receipts.ToListAsync();
    }

    public async Task<Receipt?> GetReceiptByIdAsync(int id)
    {
        return await _context.Receipts.FindAsync(id);
    }

    public async Task<Receipt> CreateReceiptAsync(Receipt receipt)
    {
        await _context.Receipts.AddAsync(receipt);
        await _context.SaveChangesAsync();
        return receipt;
    }

    public async Task<Receipt?> UpdateReceiptAsync(int id, UpdateReceiptRequestDto updateReceiptRequestDto)
    {
        var existing = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
        
        if (existing == null)
        {
            return null;
        }
        
        existing.Name = updateReceiptRequestDto.Name;
        existing.Description = updateReceiptRequestDto.Description;
        existing.CategoryId = updateReceiptRequestDto.CategoryId;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<Receipt?> DeleteReceiptAsync(Receipt receipt)
    {
       var existing = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == receipt.Id);
       if (existing == null)
       {
           return null;
       }
       _context.Receipts.Remove(existing);
       await _context.SaveChangesAsync();
       return existing;
    }
}