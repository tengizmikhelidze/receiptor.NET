using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
using receiptor.NET.Interfaces;
using receiptor.NET.Models;

namespace receiptor.NET.Repository;

public class IngredientRepository: IIngredientRepository
{
    private readonly ApplicationDBContext _context;
    
    public IngredientRepository(
        ApplicationDBContext context
        )
    {
        _context = context;
    }
    
    
    public async Task<List<Ingredient>> getAllIngredientsAsync()
    {
        return await _context.Ingredients.ToListAsync();
    }

    public async Task<Ingredient?> getIngredientByIdAsync(int id)
    {
        return await _context.Ingredients.FindAsync(id);
    }

    public async Task<Ingredient> createIngredientAsync(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
        return ingredient;
    }

    public async Task<Ingredient?> updateIngredientAsync(int id, UpdateIngredientRequestDTO updateIngredientRequestDto)
    {
        var findById = await getIngredientByIdAsync(id);
        
        if (findById == null)
        {
            return null;
        }
        
        findById.ReceiptId = updateIngredientRequestDto.ReceiptId ?? findById.ReceiptId;
        findById.Name = updateIngredientRequestDto.Name;
        findById.Quantity = updateIngredientRequestDto.Quantity;
        findById.QuantityUnit = updateIngredientRequestDto.QuantityUnit;
        
        await _context.SaveChangesAsync();
        return findById;
    }

    public async Task<Ingredient?> deleteIngredientAsync(int id)
    {
        var findById = await getIngredientByIdAsync(id);
        if (findById == null)
        {
            return null;
        }
        
        _context.Ingredients.Remove(findById);
        await _context.SaveChangesAsync();
        
        return findById;
    }
}