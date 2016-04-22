using Microsoft.VisualStudio.TestTools.UnitTesting;
using Desafio.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Security.Tests
{
    [TestClass()]
    public class CryptographyTests
    {
        [TestMethod()]
        public void ShouldReturnAValidJwtToken()
        {
            // ARRANGE

            // ACT
            var expected = Cryptography.CreateJwt(1, "teste@teste.com");

            // ASSERT
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void ShouldValidateAJwtToken()
        {
            // ARRANGE
            var actual = Cryptography.CreateJwt(1, "teste@teste.com");

            // ACT
            var expected = Cryptography.ValidateJwt(actual);

            // ASSERT
            Assert.IsNotNull(expected);
        }

        [TestMethod()]
        public void ShouldReturnAPasswordHash()
        {
            // ARRANGE
            var pass = "p4$$w0rd!!@";

            // ACT
            var expected = pass.GetPasswordHash();

            // ASSERT
            Assert.IsNotNull(expected);
            Assert.AreNotEqual("", expected);
        }

        [TestMethod()]
        public void ShouldValidateAPasswordHash()
        {
            // ARRANGE
            var pass = "p4$$w0rd!!@";
            var actual = pass.GetPasswordHash();

            // ACT
            var expected = Cryptography.VerifyHashedPassword(actual, pass);

            // ASSERT
            Assert.IsTrue(expected);
        }
    }
}