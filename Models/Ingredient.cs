using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using receiptor.NET.Enums;

namespace receiptor.NET.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        public QuantityUnit QuantityUnit { get; set; }
    }
}