using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, decimal price, int productID) : base(name, price, productID)
        {
        }
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }
        public bool InSeason { get; set; }

    }
}
