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
    public class TestTransaction
    {
        User testUser = new User("test", "test", "test@test.com", "test");
        Product testProduct = new Product("test", 11, 3);

        [Test]
        public void TestIfInsufficientExceptionIsThrown()
        {
            testUser.Balance = 10;
            testProduct.Active = true;
            BuyTransaction bt = new BuyTransaction(testUser, testProduct);
            Assert.Throws(typeof(InsufficientCreditsException),bt.Execute);
        }

        [Test]
        public void TestIfNotActiveExceptionIsThrown()
        {
            testUser.Balance = 15;
            testProduct.Active = false;
        }
    }
}
