using Microsoft.AspNetCore.Mvc;
using receiptor.NET.Data;
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
        public IActionResult GetReceipts()
        {
            var receipts = _context.Receipts.ToList()
                .Select(r => r.ToReceiptDto());
            return Ok(receipts);
        }

        [HttpGet("{id}")]
        public IActionResult GetReceipt([FromRoute] int id)
        {
            var receipt = _context.Receipts.Find(id);
            if( receipt == null)
            {
                return NotFound();
            }
            return Ok(receipt.ToReceiptDto());
        }
    }
}