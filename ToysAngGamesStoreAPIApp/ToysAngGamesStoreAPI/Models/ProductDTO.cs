using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToysAngGamesStoreAPI.Models
{
    public class ProductDTO
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Name { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Description { get; set; }
        [Range(0, 100,
            ErrorMessage = "Age must be between 0 and 100")]
        public int AgeRestriction { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string? Company { get; set; }
        [Required]
        [Range(1, 1000,
            ErrorMessage = "Price must be between 1 and 1000")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
    }
}
