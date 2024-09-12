namespace shop.Presentation.Tests;

using shop.Domain;
using shop.IBusinessLogic;
using shop.Presentation;
using shop.BusinessLogic;
using Moq;

[TestClass]
public class MainTests
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

        // Act
        Exception thrownException = null;
        try
        {
            Program.RunApplication(mockUserService.Object, existingUser.Mail, existingUser.Address, existingUser.Password);
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
        
        mockUserService
            .Setup(us => us.ValidateUser(It.IsAny<User>()))
            .Returns(false);

        // Act
        var resultUser = Program.RunApplication(mockUserService.Object, "test@example.com", "123 Main St", "Password123");

        // Assert
        Assert.AreEqual(expectedUser, resultUser!);
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

        mockUserService
            .Setup(us => us.ValidateUser(It.IsAny<User>()))
            .Returns(false);

        var userService = mockUserService.Object;

        // Act
        var resultUser = Program.RunApplication(mockUserService.Object, user.Mail, user.Address, user.Password);

        // Assert
        mockUserService.VerifyAll();
        Assert.IsTrue(resultUser!.Equals(user));
    }

    /// <summary>
    /// Ejemplo de prueba unitaria que verifica que se invoque el método DeleteUser del servicio de usuarios.
    /// </summary>
    [TestMethod]
    public void DeleteUser_ShouldInvokeDeleteUser_WhenCalled()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var user = new User { Mail = "test@example.com", Address = "123 Main St", Password = "Password123" };

        mockUserService
            .Setup(us => us.CreateUser(user.Mail, user.Address, user.Password))
            .Returns(user);

        mockUserService
            .Setup(us => us.ValidateUser(It.IsAny<User>()))
            .Returns(true);

        mockUserService
            .Setup(us => us.DeleteUser(It.IsAny<int>(), It.IsAny<Guid>()));

        // Act
        var resultUser = Program.RunApplication(mockUserService.Object, user.Mail, user.Address, user.Password);

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
        var mockUserService = new Mock<IUserService>();
        var user = new User { Mail = "test@example.com", Address = "123 Main St", Password = "Password123" };

        mockUserService
            .Setup(us => us.CreateUser(user.Mail, user.Address, user.Password))
            .Returns(user);

        mockUserService
            .Setup(us => us.ValidateUser(It.IsAny<User>()))
            .Returns(true);

        mockUserService
            .Setup(us => us.DeleteUser(It.IsAny<int>(), It.IsAny<Guid>()))
            .Throws(new ResourceNotFoundException("Such user does not exist."));

        var userService = mockUserService.Object;

        // Act
        Exception thrownException = null;
        try
        {
            Program.RunApplication(mockUserService.Object, user.Mail, user.Address, user.Password);
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