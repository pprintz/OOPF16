using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Eksamensopgave2016
{
    public class ProductList
    {
        public static Dictionary<int, Product> LoadAAUProducts()
        {
            string htmlTags = "<.*?>";
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            string productsInOneLine = Eksamensopgave2016.Properties.Resources.products;
            using (StringReader reader = new StringReader(productsInOneLine))
            {
                string lineBuffer;
                while ((lineBuffer = reader.ReadLine()) != null)
                {
                    string[] productValues =
                       Regex.Replace(lineBuffer, htmlTags, string.Empty).Replace(@"""", string.Empty).Split(';');
                    int secondLastDigitIndex = productValues[2].Length - 2;
                    if (productValues[0] != "id")
                    {
                        if (productValues[2].Length > 2)
                        {
                            products.Add(Int32.Parse(productValues[0]), new SeasonalProduct(productValues[1],
                                Decimal.Parse(productValues[2].Insert(secondLastDigitIndex, ",")),
                                Int32.Parse(productValues[0]))
                            { Active = Convert.ToBoolean(Int32.Parse(productValues[3])) });
                        }
                    }
                }
            }
            return products;
        }
    }
}
