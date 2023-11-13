using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webshopmen.model
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public decimal priceOld { get; set; }
        public decimal priceSale { get; set; }
        public string description { get; set; }
        public byte classify { get; set; }
        public string imageURL { get; set; }
        public int discountPercentage
        {
            get
            {
                if (priceOld > 0)
                {
                    decimal discount = 100 - (priceSale / priceOld) * 100;
                    return (int)discount;
                }
                return 0;
            }
        }
        public List<byte> size  { get; set; }
        public List<string> color { get; set; }
        public List<int> quantity { get; set; }
        public List<string> relatedImages { get; set; }
    }
}