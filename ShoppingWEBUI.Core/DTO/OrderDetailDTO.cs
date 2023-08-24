﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.DTO
{
    public class OrderDetailDTO
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double UnitPrice { get; set; }
        public double? Quantity { get; set; }
        public double? Discount { get; set; }
        public Guid Guid { get; set; }

    }
}
