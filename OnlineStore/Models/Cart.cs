using OnlineStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string SellerId { get; set; }

    }
}
