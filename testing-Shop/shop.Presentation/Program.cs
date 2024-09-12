namespace shop.Presentation;

using Microsoft.Extensions.DependencyInjection;
using shop.Domain;
using shop.IBusinessLogic;
using shop.BusinessLogic;

public class Program
{
    private static void Main(string[] args)
    {
        var services = CreateServices();

        Console.WriteLine("Creating user...");

        IUserService app = services.GetRequiredService<IUserService>();
        RunApplication(app, "test@email", "address", "password");
    }

    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IUserService>(new UserService())
            .BuildServiceProvider();

        return serviceProvider;
    }

    public static User? RunApplication(IUserService app, string email, string address, string password)
    {
        User user = app.CreateUser(email, address, password);

        Console.WriteLine($"User created: {user.Id}");

        if (app.ValidateUser(user))
        {
            app.DeleteUser(user.Id, Guid.NewGuid());
            return null;
        }

        return user;
    }
}
