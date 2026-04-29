using Microsoft.AspNetCore.Mvc;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
using receiptor.NET.Interfaces;
using receiptor.NET.Mappers;

namespace receiptor.NET.Controllers;

[Route("api/Ingredient")]
[ApiController]
public class IngredientController: ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IIngredientRepository _ingredientRepository;
    
    public IngredientController(
        ApplicationDBContext context,
        IIngredientRepository ingredientRepository
        )
    {
        _context = context;
        _ingredientRepository = ingredientRepository;
    }

    [HttpGet("/api/Ingredients")]
    public async Task<IActionResult> GetIngredients()
    {
        var ingredients = await _ingredientRepository.getAllIngredientsAsync();
        var ingredientsDTO = ingredients.Select(i => i.ToIngredientDTO()); 
        
        return Ok(ingredientsDTO);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredient([FromRoute] int id)
    {
        var ingredient = await _ingredientRepository.getIngredientByIdAsync(id);
        if( ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient.ToIngredientDTO());
    }
    
    [HttpPost] 
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientRequestDTO createIngredientRequestDTO)
    {
        var ingredient = createIngredientRequestDTO.toIngredientFromCreateDTO();
        await _ingredientRepository.createIngredientAsync(ingredient);
        
        return CreatedAtAction(nameof(GetIngredient), new {id = ingredient.Id}, ingredient.ToIngredientDTO());
    }
}