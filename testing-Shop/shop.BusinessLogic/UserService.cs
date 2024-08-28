using shop.IBusinessLogic;
using shop.Domain;

namespace shop.BusinessLogic;

public class UserService : IUserService
{
    public UserService() { }

    public User CreateUser(string email, string address, string password)
    {
        return new User
        {
            Mail = email,
            Address = address,
            Password = password
        };
    }

    public void DeleteUser(int userId, Guid adminToken)
    {
        if (userId <= 0)
        {
            throw new ResourceNotFoundException("Such user does not exist.");
        }

        // Lógica para eliminar al usuario...
    }

    public bool ValidateUser(User user)
    {
        return user.Mail.Contains("@");
    }
}

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message) : base(message) { }
}

public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException(string message) : base(message) { }
}
