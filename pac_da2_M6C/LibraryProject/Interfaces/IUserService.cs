using LibraryProject.Entities;
namespace LibraryProject.Interfaces
{
    public interface IUserService
    {
        User CreateUser(User user);
        User? GetUserById(int id);
        IEnumerable<User> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}