using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class Product  
    {
        public Product(string name, decimal price, int productID)
        {
            Price = price;
            Name = name;
            ProductID = productID;
        }

        public int ProductID { get; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Name can't be null");
                }
                _name = value;
            }
        }

        public decimal Price { get; set; }
        public bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; }
        public override string ToString()
        {
            return $"{ProductID}  {Name}  {Price}Kr";
        }
    }
}
