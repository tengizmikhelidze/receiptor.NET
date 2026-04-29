using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
using receiptor.NET.Interfaces;
using receiptor.NET.Mappers;
using receiptor.NET.Repository;

namespace receiptor.NET.Controllers
{
    [Route("api/Receipt")]
    [ApiController]
    public class ReceiptController: ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IReceiptRepository _receiptRepository;

        public ReceiptController(
            ApplicationDBContext context, 
            IReceiptRepository receiptRepository
            )
        {
            _context = context;
            _receiptRepository = receiptRepository;
        }

        [HttpGet("/api/Receipts")]
        public async Task<IActionResult> GetReceipts()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receipts = await _receiptRepository.GetAllReceiptsAsync();

            var receiptsDto = receipts.Select(r => r.ToReceiptDto());
            
            return Ok(receiptsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetReceipt([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var receipt = await _receiptRepository.GetReceiptByIdAsync(id);
            if( receipt == null)
            {
                return NotFound();
            }
            return Ok(receipt.ToReceiptDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] CreateReceiptRequestDto bodyValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receiptModel = bodyValue.toReceiptFromCreateDTO();
            await _receiptRepository.CreateReceiptAsync(receiptModel);
            return CreatedAtAction(nameof(GetReceipt), new {id = receiptModel.Id}, receiptModel.ToReceiptDto());
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReceiptRequestDto bodyValue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReceipt = await _receiptRepository.GetReceiptByIdAsync(id);
            
            if (existingReceipt == null)
            {
                return NotFound();
            }
            
            await _receiptRepository.UpdateReceiptAsync(existingReceipt.Id, bodyValue);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReceipt = await _receiptRepository.DeleteReceiptAsync(id);
            if (existingReceipt == null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    
        [HttpGet("GetReceiptByIngredient/{ingredientId}")]
        public async Task<IActionResult> GetIngredientReceipt([FromRoute] int ingredientId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receipt = await _receiptRepository.getReceiptByIngredientIdAsync(ingredientId);
            if (receipt == null)
            {
                return NotFound();
            }
        
            return Ok(receipt);
        }
    }
}