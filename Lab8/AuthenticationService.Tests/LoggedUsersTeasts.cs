using Lab8;
using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LoginTests
    {
        private AuthService _authService = new AuthService();

        [TestInitialize]
        public void Setup()
        {
            _authService.Register("SuperUser", "StrongPass_123");
            _authService.Register("YetAnotherSuperUser", "StrongPass_123");
        }

        [TestMethod]
        public void Login_NonExistentUsername_ReturnsFalse()
        {
            bool result = _authService.Login("User_no", "password");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_InvalidPassword_ReturnsFalse()
        {
            bool result = _authService.Login("SuperUser", "password");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Login_ValidCredentials_ReturnsTrue()
        {
            bool result = _authService.Login("SuperUser", "StrongPass_123");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Login_ValidCredentialsSecondAttempt_ReturnsFalse()
        {
            _authService.Login("SuperUser", "StrongPass_123");
            bool result = _authService.Login("SuperUser", "StrongPass_123");

            Assert.IsFalse(result); 
        }
    }

