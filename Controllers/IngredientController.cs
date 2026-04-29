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
    private readonly IReceiptRepository _receiptRepository;
    
    public IngredientController(
        ApplicationDBContext context,
        IIngredientRepository ingredientRepository,
        IReceiptRepository receiptRepository
        )
    {
        _context = context;
        _ingredientRepository = ingredientRepository;
        _receiptRepository = receiptRepository;
    }

    [HttpGet("/api/Ingredients")]
    public async Task<IActionResult> GetIngredients()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var ingredients = await _ingredientRepository.getAllIngredientsAsync();
        var ingredientsDto = ingredients.Select(i => i.ToIngredientDTO()); 
        
        return Ok(ingredientsDto);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetIngredientByID([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var ingredient = await _ingredientRepository.getIngredientByIdAsync(id);
        if( ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient.ToIngredientDTO());
    }
    
    [HttpPost("{receiptId:int}")] 
    public async Task<IActionResult> CreateIngredient([FromRoute] int receiptId, [FromBody] CreateIngredientRequestDTO createIngredientRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var receipt = await _receiptRepository.ReceiptExistsAsync(receiptId);
        if (!receipt)
        {
            return NotFound();
        }
        
        var ingredientFromCreateDto = createIngredientRequestDto.toIngredientFromCreateDTO(receiptId);
        await _ingredientRepository.createIngredientAsync(ingredientFromCreateDto);
        
        return CreatedAtAction(nameof(GetIngredientByID), new {id = ingredientFromCreateDto.Id}, ingredientFromCreateDto.ToIngredientDTO());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient([FromRoute] int id,
        [FromBody] UpdateIngredientRequestDTO updateIngredientRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var receiptExists = await _receiptRepository.ReceiptExistsAsync(updateIngredientRequestDto.ReceiptId.Value);
        if (!receiptExists)        {
            return NotFound("Receipt does not exist");
        }
        
        var ingredient = await _ingredientRepository.updateIngredientAsync(id, updateIngredientRequestDto);
        if (ingredient == null)
        {
            return NotFound();
        }
        
        return Ok(ingredient);
    }
}