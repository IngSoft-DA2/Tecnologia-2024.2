namespace shop.BusinessLogic.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using shop.Domain;
    using shop.BusinessLogic;


[TestClass]
public class UserServiceTests
{
 [TestMethod]
public void CreateUser_ShouldThrowException_WhenUserAlreadyExists()
{
    // Arrange
    var existingUser = new User { Mail = "existing@example.com", Address = "123 Main St", Password = "Password123" };
    var mockUserService = new Mock<UserService>(MockBehavior.Strict);

    mockUserService
        .Setup(us => us.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
        .Throws(new ResourceAlreadyExistsException("User already exists."));

    var userLogic = new UserService(mockUserService.Object);

    // Act
    Exception thrownException = null;
    try
    {
        userLogic.CreateUser(existingUser.Mail, existingUser.Address, existingUser.Password);
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
}
}
