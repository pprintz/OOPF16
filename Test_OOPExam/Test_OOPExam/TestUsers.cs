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
    public class TestUsers
    {
        [Test]
        public void TestLocalWhenFirstIsDash()
        {
            Assert.False(User.IsEmailValid("-asd@asd.dk"));
        }

        [Test]
        public void TestLocalWhenFirstIsDot()
        {
            Assert.False(User.IsEmailValid(".asd@asd.dk"));
        }

        [Test]
        public void TestLocalWhenLastIsDot()
        {
            Assert.False(User.IsEmailValid("asd.@asd.dk"));
        }

        [Test]
        public void TestLocalWhenLastIsDash()
        {
            Assert.False(User.IsEmailValid("asd-@asd.dk"));
        }

        [Test]
        public void TestDomainWhenFirstIsDash()
        {
            Assert.False(User.IsEmailValid("asd@-asd.dk"));
        }

        [Test]
        public void TestDomainWhenFirstIsDot()
        {
            Assert.False(User.IsEmailValid("asd@.asd.dk"));
        }

        [Test]
        public void TestDomainWhenLastIsDash()
        {
            Assert.False(User.IsEmailValid("asd@asd.dk-"));
        }

        [Test]
        public void TestDomainWhenLastIsDot()
        {
            Assert.False(User.IsEmailValid("asd@asd.dk."));
        }
        [Test]
        public void TestWhenDomainContainsCorrectChars()
        {
            Assert.False(User.IsEmailValid("asd@%!).com"));
        }

        [Test]
        public void TestWhenEmailContainsNoDots()
        {
            Assert.False(User.IsEmailValid("asd@asddk"));
        }
        [Test]
        public void TestWhenEmailHasMoreThanOneAtSign()
        {
            Assert.False(User.IsEmailValid("asd@as@d.dk"));
            Assert.False(User.IsEmailValid("asd@asd.dk@"));
        }

        [Test]
        public void TestLocalPartWhenContainsSpecialSigns()
        {
            Assert.False(User.IsEmailValid("a/sd@asd.dk"));
        }

        [Test]
        public void TestUsername()
        {
            Assert.False(User.IsUsernameValid("asd#asd"));
            Assert.True(User.IsUsernameValid("asd_asd"));
            Assert.True(User.IsUsernameValid("a9sd_asd"));
            Assert.False(User.IsUsernameValid("Aa9sd_asd"));
            Assert.False(User.IsUsernameValid("Aasdasd"));
        }

        [Test]
        public void TestIfFirstNameAndLastNameCanBeNull()
        {
            User testUser = new User(null, null, "asd@asd.com", "asd");
            Assert.IsNotNull(testUser.Firstname);
            Assert.IsNotNull(testUser.Lastname);
        }
    }

}
