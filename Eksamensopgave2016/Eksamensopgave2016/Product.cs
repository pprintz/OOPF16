/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
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
                _name = "..";
                if (value != null)
                {
                    _name = value;
                }
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
