using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
    [Table("promotion-items")]
    public class PromotionItems
    {

        public int ID { get; set; }

        public int PromotionID { get; set; }

        [ForeignKey("PromotionID")]
        public Promotion Promotion { get; set; }

        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
