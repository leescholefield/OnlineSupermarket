using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
        
    [Table("promotions")]
    public class Promotion
    {

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string ImageName { get; set; }

        [NotMapped]
        public List<Product> Items { get; set; } = new List<Product>();
    }
}
