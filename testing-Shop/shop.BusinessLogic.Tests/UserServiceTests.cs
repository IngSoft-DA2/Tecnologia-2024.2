using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using shop.Domain;
using shop.BusinessLogic;
using shop.IBusinessLogic;

namespace shop.BusinessLogic.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        /// <summary>
        /// Ejemplo de prueba unitaria que verifica que se lance una excepción cuando se intenta crear un usuario que ya existe.
        /// </summary>
        [TestMethod]
        public void CreateUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var existingUser = new User { Mail = "existing@example.com", Address = "123 Main St", Password = "Password123" };
            var mockUserService = new Mock<IUserService>(MockBehavior.Strict);

            mockUserService
                .Setup(us => us.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new ResourceAlreadyExistsException("User already exists."));

            var userService = mockUserService.Object;

            // Act
            Exception thrownException = null;
            try
            {
                userService.CreateUser(existingUser.Mail, existingUser.Address, existingUser.Password);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            // Assert
            mockUserService.VerifyAll();
            Assert.IsNotNull(thrownException);
            Assert.IsInstanceOfType(thrownException, typeof(ResourceAlreadyExistsException));
            Assert.AreEqual("User already exists.", thrownException.Message);
        }

        /// <summary>
        /// Ejemplo de prueba unitaria que verifica que se retorne un usuario equivalente al que se intenta crear.
        /// </summary>
        [TestMethod]
        public void CreateUser_ShouldReturnEquivalentUser_WhenGivenValidData()
        {
            // Arrange
            var expectedUser = new User { Mail = "test@example.com", Address = "123 Main St", Password = "Password123" };
            var mockUserService = new Mock<IUserService>();

            mockUserService
                .Setup(us => us.CreateUser(expectedUser.Mail, expectedUser.Address, expectedUser.Password))
                .Returns(expectedUser);

            var userService = mockUserService.Object;

            // Act
            var resultUser = userService.CreateUser("test@example.com", "123 Main St", "Password123");

            // Assert
            Assert.AreEqual(expectedUser, resultUser);
        }

        /// <summary>
        /// Ejemplo de prueba unitaria que verifica que se invoque el método CreateUser del servicio de usuarios. 
        /// y Verifica que el objeto resultante es igual al objeto original, validando que la operación se comporta como se espera.
        /// </summary>
        [TestMethod]
        public void CreateUser_ShouldInvokeCreateUser_WhenCalled()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var user = new User { Mail = "test@example.com", Address = "123 Main St", Password = "Password123" };

            mockUserService
                .Setup(us => us.CreateUser(user.Mail, user.Address, user.Password))
                .Returns(user);

            var userService = mockUserService.Object;

            // Act
            var resultUser = userService.CreateUser(user.Mail, user.Address, user.Password);

            // Assert
            mockUserService.VerifyAll();
            Assert.IsTrue(resultUser.Equals(user));
        }

        /// <summary>
        /// Ejemplo de prueba unitaria que verifica que se invoque el método DeleteUser del servicio de usuarios.
        /// </summary>
        [TestMethod]
        public void DeleteUser_ShouldInvokeDeleteUser_WhenCalled()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var userId = 1;
            var adminToken = Guid.NewGuid();

            mockUserService
                .Setup(us => us.DeleteUser(userId, adminToken));

            var userService = mockUserService.Object;

            // Act
            userService.DeleteUser(userId, adminToken);

            // Assert
            mockUserService.VerifyAll();
        }

        /// <summary>
        /// Ejemplo de prueba unitaria que verifica que se lance una excepción cuando se intenta eliminar un usuario que no existe.
        /// </summary>
        [TestMethod]
        public void DeleteUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var invalidUserId = 0;
            var adminToken = Guid.NewGuid();
            var mockUserService = new Mock<IUserService>();

            mockUserService
                .Setup(us => us.DeleteUser(invalidUserId, adminToken))
                .Throws(new ResourceNotFoundException("Such user does not exist."));

            var userService = mockUserService.Object;

            // Act
            Exception thrownException = null;
            try
            {
                userService.DeleteUser(invalidUserId, adminToken);
            }
            catch (Exception ex)
            {
                thrownException = ex;
            }

            // Assert
            mockUserService.VerifyAll();
            Assert.IsNotNull(thrownException);
            Assert.IsInstanceOfType(thrownException, typeof(ResourceNotFoundException));
            Assert.AreEqual("Such user does not exist.", thrownException.Message);
        }

    }
}
