using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class ProductList
    {
        public List<Product> LoadProducts()
        {
            string htmlTags = "<.*?>";
            List<Product> products = new List<Product>();
            var reader = new StreamReader(File.OpenRead(Environment.CurrentDirectory + @"/products.csv"));
            while (!reader.EndOfStream)
            {
                List<String> productValues =
                    Regex.Replace(reader.ReadLine(), htmlTags, string.Empty).Split(';').ToList();
                if (productValues[1].Contains('"'))
                {
                    products.Add(new Product(productValues[1],
                        Decimal.Parse(productValues[2]),
                        Int32.Parse(productValues[0])));
                }
            }
            return products;
        }
    }
}
