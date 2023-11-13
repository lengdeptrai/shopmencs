using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webshopmen.model
{
    public class ProductParameter
    {
        public int productParameterID { get; set; }
        public int productID { get; set; }
        public string color { get; set; }
        public byte size { get; set; }
        public int quantity { get; set; }
        public string ImgUrl { get; set; }
    }
}