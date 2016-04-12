using NUnit.Framework;
using Eksamensopgave2016;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Test_OOPExam
{
    [TestFixture]
    public class TestProduct
    {
        [Test]
        public void TestIfProductNameCanBeNull()
        {
            Product testProduct = new Product(null, 10, 30);
            Assert.IsNotNull(testProduct.Name);
        }
    }
}
