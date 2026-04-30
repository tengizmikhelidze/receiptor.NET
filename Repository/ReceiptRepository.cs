using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
using receiptor.NET.Enums;
using receiptor.NET.Helpers;
using receiptor.NET.Interfaces;
using receiptor.NET.Models;

namespace receiptor.NET.Repository;

public class ReceiptRepository: IReceiptRepository
{
    private readonly ApplicationDBContext _context;
    private readonly IIngredientRepository _ingredientRepository;
    
    public ReceiptRepository(
        ApplicationDBContext context,
        IIngredientRepository ingredientRepository
    )
    {
        _context = context;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<List<Receipt>> GetAllReceiptsAsync(ReceiptQueryObject receiptQuery)
    {
        var query = _context.Receipts.AsNoTracking().Include(i => i.Ingredients).AsQueryable();
        if(!string.IsNullOrWhiteSpace(receiptQuery.Name))
        {
            var nameQuery = receiptQuery.Name.Trim();
            query = query.Where(r => r.Name.Contains(nameQuery));
        }

        if (!string.IsNullOrWhiteSpace(receiptQuery.Description))
        {
            var descriptionQuery = receiptQuery.Description.Trim();
            query = query.Where(r => r.Description.Contains(descriptionQuery));
        }
        
        query = ApplySorting(query, receiptQuery.Sort);
        return await query.ToListAsync();
    }

    public async Task<Receipt?> GetReceiptByIdAsync(int id)
    {
        return await _context.Receipts.Include(i => i.Ingredients).FirstOrDefaultAsync(r=> r.Id == id);
    }

    public async Task<Receipt> CreateReceiptAsync(Receipt receipt)
    {
        await _context.Receipts.AddAsync(receipt);
        await _context.SaveChangesAsync();
        return receipt;
    }

    public async Task<Receipt?> UpdateReceiptAsync(int id, UpdateReceiptRequestDto updateReceiptRequestDto)
    {
        var findById = await GetReceiptByIdAsync(id);
        
        if (findById == null)
        {
            return null;
        }
        
        _context.Entry(findById).CurrentValues.SetValues(updateReceiptRequestDto);
        
        await _context.SaveChangesAsync();
        return findById;
    }

    public async Task<Receipt?> DeleteReceiptAsync(int id)
    {
       var existing = await GetReceiptByIdAsync(id);
       if (existing == null)
       {
           return null;
       }
       _context.Receipts.Remove(existing);
       await _context.SaveChangesAsync();
       return existing;
    }
    
    public async Task<bool> ReceiptExistsAsync(int receiptId)
    {
        var existing = await _context.Receipts.AnyAsync(r => r.Id == receiptId);
        return existing;
    }

    public async Task<Receipt?> getReceiptByIngredientIdAsync(int ingredientId)
    {
        var ingredient = await _ingredientRepository.getIngredientByIdAsync(ingredientId);
        if (ingredient == null)
        {
            return null;
        }
        
        return await _context.Receipts.FindAsync(ingredient.ReceiptId);
    }

    private static IQueryable<Receipt> ApplySorting(IQueryable<Receipt> query, List<ReceiptSort> sorts)
    {
        if( sorts == null || sorts.Count == 0)
        {
            return query;
        }
        
        IOrderedQueryable<Receipt>? orderedQuery = null;

        foreach (var sort in sorts)
        {
            Expression<Func<Receipt, object>> keySelector = sort.Field switch
            {
                ReceiptSortField.Name => r => r.Name,
                ReceiptSortField.Description => r => r.Description,
                ReceiptSortField.CreatedAt => r => r.CreatedAt,
                ReceiptSortField.ModifiedAt => r => r.ModifiedAt,
                ReceiptSortField.Quantity => r => r.Ingredients.Count
            };
            
            var isDesc = sort.SortBy == SortBy.Desc;

            if (orderedQuery == null)
            {
                orderedQuery = isDesc ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
            }
            else
            {
                orderedQuery = isDesc ? orderedQuery.ThenByDescending(keySelector) : orderedQuery.ThenBy(keySelector);
            }
        }
        
        return orderedQuery ?? query;
        
    }
}