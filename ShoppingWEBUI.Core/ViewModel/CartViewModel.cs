using ShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.ViewModel
{
    public class CartViewModel
    {
        public List<CartDTO> CartItems { get; set; }
        public double TotalPrice { get; set; }
    }
    
}
