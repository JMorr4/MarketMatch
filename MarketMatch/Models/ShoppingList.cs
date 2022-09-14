using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace MarketMatch.Models
{
    [Index(nameof(UserListId), nameof(ProductId), IsUnique = true)]
    public partial class ShoppingList
    {
        [Key]
        public int ListId { get; set; }

        [Required]
        [HiddenInput]
        [MaxLength(36)]
        public string UserListId { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 20)]
        public int Quantity { get; set; }
    }
}
