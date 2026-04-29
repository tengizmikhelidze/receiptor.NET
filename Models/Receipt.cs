using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using receiptor.NET.Enums;

namespace receiptor.NET.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(255, ErrorMessage = "The {0} must be at most {1} characters long.")]
        public string Description { get; set; } = string.Empty;

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

        public QuantityUnit QuantityUnit { get; set; }
        

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal AveragePrice { get; set; }
    }
}