using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Productfront.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public Category category { get; set; }

    }
}