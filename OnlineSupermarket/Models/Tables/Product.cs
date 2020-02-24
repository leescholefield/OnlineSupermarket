using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
    [Table("products")]
    public class Product
    {

        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$"), Required]
        public string Category { get; set; }

        [Range(0, 5), Required]
        public int Rating { get; set; }

        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string ImageName { get; set; }

        public int Popularity { get; set; }

        [NotMapped]
        public bool InBasket { get; set; } = false;

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   ID == product.ID &&
                   Name == product.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Name);
        }
    }
}
