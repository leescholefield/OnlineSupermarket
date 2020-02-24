using System.Collections.Generic;

namespace OnlineSupermarket.Models
{
    public class HomeViewModel : BaseViewModel
    {

        public IEnumerable<Promotion> Promotions { get; set; }

    }
}
