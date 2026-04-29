using receiptor.NET.Models;

namespace receiptor.NET.Interfaces;

public interface IIngredientRepository
{
    Task<List<Ingredient>> getAllIngredientsAsync();
    Task<Ingredient?> getIngredientByIdAsync(int id);
    Task<Ingredient> createIngredientAsync(Ingredient ingredient);
}