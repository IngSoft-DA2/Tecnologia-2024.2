namespace shop.IBusinessLogic;

using shop.Domain;

public interface IUserService
{
    User CreateUser(string email, string address, string password);
    void DeleteUser(int userId, Guid adminToken);
    bool ValidateUser(User user);
}
