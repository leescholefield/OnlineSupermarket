

using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineSupermarket.Models
{
    public class BaseViewModel
    {

        public int NumberCheckoutItems { get; set; }

        public SelectList Categories { get; set; }
    }
}
