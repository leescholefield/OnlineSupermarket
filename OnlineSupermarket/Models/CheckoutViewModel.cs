using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSupermarket.Models
{
    public class CheckoutViewModel : BaseViewModel
    {
        public Dictionary<Product, int> ProductDictionary { get; set; }

        [Column(TypeName = "decimal(18, 2)"), DataType(DataType.Currency)]
        public double Total { get; set; }
    }
}
