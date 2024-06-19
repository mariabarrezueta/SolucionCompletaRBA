using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppmovilRebe.Models
{
    public class BurgerRB
    {
        
        public int burguerId { get; set; }
        public string? name { get; set; }
        public bool withCheese { get; set; }
        public decimal precio { get; set; }
        public object[]? promos { get; set; }
    }


}
