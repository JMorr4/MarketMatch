using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketMatch.Models
{
    [Index(nameof(SupermarketId), nameof(ProductId), nameof(PriceDate), IsUnique = true)]
    public partial class ProductPrice
    {
        [Key]
        public int PriceId { get; set; }

        [Required]
        public int SupermarketId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PriceDate { get; set; }

        [Required]
        public float Price { get; set; }
    }
}