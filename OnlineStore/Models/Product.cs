using OnlineStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        [Range(0,90,ErrorMessage ="Discount must be between ( 0%, 90% )")]
        public int Discount { get; set; }
        [AllowNull]
        public byte[]? Image { get; set; }
        [Required]
        [Range(1,10000,ErrorMessage ="Quantity must be at least 1") ]
        public int Quantity { get; set; }

        [MaxLength(50)]
        [Required]
        public string Category { get; set; }

        [ForeignKey("Seller")]
        public string SellerId { get; set; }

    }
}
