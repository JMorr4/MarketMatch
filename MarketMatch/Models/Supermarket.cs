using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketMatch.Models
{
    public partial class Supermarket
    {
        [Key]
        public int SupermarketId { get; set; }

        [Required]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z& ]+$", ErrorMessage = "Use letters, spaces or '&' only please")]
        public string Name { get; set; } = null!;

        [MaxLength(30)]
        public string Logo { get; set; } = null!;

        [Required]
        [NotMapped]
        public IFormFile LogoFile { get; set; } = null!;

        public List<ProductPrice> ProductPrices { get; set; } = null!;
    }
}
