using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webshopmen.model
{
    public class UserShop
    {
        public int userID { get; set; }
        public string Name { get; set; }
        public string userName {get ; set;}
        public string password { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public bool accessible { get; set; }

    }
}