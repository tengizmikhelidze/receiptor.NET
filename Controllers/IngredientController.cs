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
        var ingredientsDto = ingredients.Select(i => i.ToIngredientDTO()); 
        
        return Ok(ingredientsDto);
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
    public async Task<IActionResult> CreateIngredient([FromBody] CreateIngredientRequestDTO createIngredientRequestDto)
    {
        var ingredient = createIngredientRequestDto.toIngredientFromCreateDTO();
        await _ingredientRepository.createIngredientAsync(ingredient);
        
        return CreatedAtAction(nameof(GetIngredient), new {id = ingredient.Id}, ingredient.ToIngredientDTO());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient([FromRoute] int id,
        [FromBody] UpdateIngredientRequestDTO updateIngredientRequestDto)
    {
        var ingredient = await _ingredientRepository.updateIngredientAsync(id, updateIngredientRequestDto);
        if (ingredient == null)
        {
            return NotFound();
        }
        
        return Ok(ingredient);
    }
}