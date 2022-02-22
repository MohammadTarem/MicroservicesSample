using System.ComponentModel.DataAnnotations;

namespace Products
{
    public class ProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
