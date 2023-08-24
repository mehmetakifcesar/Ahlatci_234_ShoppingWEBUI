using ShoppingWEBUI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.ViewModel
{
    public class OrderViewModel
    {
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public List<ProductDTO> Products { get; set; }
        public List<CategoryDTO> Categories { get; set; }
       // public List<ProductViewModel> ProductsViewModel { get; set; }
    }
}
