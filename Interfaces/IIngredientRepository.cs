using receiptor.NET.DTOs;
using receiptor.NET.Models;

namespace receiptor.NET.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> getAllIngredientsAsync();
    Task<Ingredient?> getIngredientByIdAsync(int id);
    Task<Ingredient> createIngredientAsync(Ingredient ingredient);
    Task<Ingredient?> updateIngredientAsync(int id, UpdateIngredientRequestDTO ingredient);
    Task<Ingredient?> deleteIngredientAsync(int id);
    Task<Receipt?> getIngredientReceiptAsync(int id);
}