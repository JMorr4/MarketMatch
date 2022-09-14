using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketMatch.Models
{
    [Index(nameof(Name), IsUnique = false)]
    [Index(nameof(Brand), IsUnique = false)]
    public partial class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        [MaxLength(30)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string Category { get; set; } = null!;

        public List<ProductPrice> ProductPrices { get; set; } = null!;
        public List<ShoppingList> ShoppingLists { get; set; } = null!;
    }
}
