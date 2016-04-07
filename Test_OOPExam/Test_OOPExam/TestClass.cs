using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eksamensopgave2016;

namespace Test_OOPExam
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

        [Test]
        public void TestDomainIfFirstIsDash()
        {
            User test = new User("Peter", "madsen", "asd@-hot.dk", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestDomainIfFirstIsDot()
        {
            User test = new User("Peter", "madsen", "asd@.hot.dk", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestDomainIfLastIsDash()
        {
            User test = new User("Peter", "madsen", "asd@hot.dk-", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestDomainIfLastIsDot()
        {
            User test = new User("Peter", "madsen", "asd-@hot.dk.", "ppppp");
            Assert.Null(test.Email);
        }
        [Test]
        public void TestIfDomainContainsCorrectChars()
        {
            User test = new User("Peter", "madsen", "asd@hot.dk.", "ppppp");
            Assert.Null(test.Email);
        }

        [Test]
        public void TestWhenEmailContainsNoDots()
        {
            User test = new User("Peter", "madsen", "asd@hotdk.", "ppppp");
            Assert.Null(test.Email);

        }
    }
}