using receiptor.NET.Models;

namespace receiptor.NET.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> getAllIngredientsAsync();
    Task<Ingredient> getIngredientByIdAsync();
    Task<Ingredient> createIngredientAsync();
    Task<Ingredient> updateIngredientAsync();
    Task<Ingredient> deleteIngredientAsync(int id);
}