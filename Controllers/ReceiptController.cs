using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using receiptor.NET.Data;

namespace receiptor.NET.Controllers
{
    [Route("api/Receipt")]
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
            var receipts = _context.Receipts.ToList();
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
            return Ok(receipt);
        }
    }
}