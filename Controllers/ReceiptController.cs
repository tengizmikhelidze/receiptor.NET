using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using receiptor.NET.Data;
using receiptor.NET.DTOs;
using receiptor.NET.Mappers;

namespace receiptor.NET.Controllers
{
    [Route("api/Receipts")]
    [ApiController]
    public class ReceiptController: ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public ReceiptController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetReceipts()
        {
            var receipts = await _context.Receipts.ToListAsync();

            var receiptsDto = receipts.Select(r => r.ToReceiptDto());
            
            return Ok(receiptsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReceipt([FromRoute] int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if( receipt == null)
            {
                return NotFound();
            }
            return Ok(receipt.ToReceiptDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReceipt([FromBody] CreateReceiptRequestDto bodyValue)
        {
            var receiptModel = bodyValue.toReceiptFromCreateDTO();
            await _context.Receipts.AddAsync(receiptModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReceipt), new {id = receiptModel.Id}, receiptModel.ToReceiptDto());
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReceiptRequestDto bodyValue)
        {
            var existingReceipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
            
            if (existingReceipt == null)
            {
                return NotFound();
            }

            existingReceipt.Name = bodyValue.Name;
            existingReceipt.Description = bodyValue.Description;
            existingReceipt.CategoryId = bodyValue.CategoryId;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var existingReceipt = await _context.Receipts.FirstOrDefaultAsync(r => r.Id == id);
            if (existingReceipt == null)
            {
                return NotFound();
            }
            _context.Receipts.Remove(existingReceipt);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}