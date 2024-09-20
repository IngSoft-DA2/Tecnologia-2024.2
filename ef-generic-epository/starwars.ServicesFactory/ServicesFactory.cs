using starwars.BusinessLogic;
using starwars.DataAccess;
using starwars.IDataAccess;
using Microsoft.Extensions.DependencyInjection;
using starwars.IBusinessLogic;
using starwars.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace starwars.ServicesFactory;

public class ServicesFactory
{

        public ServicesFactory() { }

        public void RegistrateServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<DbContext, StarwarsContext>();
            serviceCollection.AddScoped<IGenericRepository<Character>, CharacterManagment>();
        }
}
