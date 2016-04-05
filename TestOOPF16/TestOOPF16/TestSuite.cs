using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Eksamensopgave2016;

namespace TestOOPF16
{
    [TestFixture]
    public class TestSuite
    {
        [Test]
        public void TestLocalIfFirstIsDash()
        {
            User test = new User("Peter", "madsen", "-asd@hot.dk", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestLocalIfFirstIsDot()
        {
            User test = new User("Peter", "madsen", ".asd@hot.dk", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestLocalIfLastIsDot()
        {
            User test = new User("Peter", "madsen", "asd.@hot.dk", "ppppp");
            Assert.Null(test.Email);
        }
        [Test]
        public void TestLocalIfLastIsDash()
        {
            User test = new User("Peter", "madsen", "asd-@hot.dk", "ppppp");
            Assert.Null(test.Email);
        }
    }
}
