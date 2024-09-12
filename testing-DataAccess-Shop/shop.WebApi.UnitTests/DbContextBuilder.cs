using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace shop.WebApi.UnitTests
{
    internal sealed class DbContextBuilder
    {
        private static SqliteConnection? _connection;

        private static SqliteConnection BuildConnection()
        {
            if( _connection == null )
            {
                _connection = new SqliteConnection("Data Source=:memory:");
            }

            return _connection;
        }

        public static ProductTestDbContext BuildTestDbContext()
        {
            var connection = BuildConnection();
            var options = new DbContextOptionsBuilder<ProductTestDbContext>()
                .UseSqlite(connection)
                .Options;

            connection.Open();

            var context = new ProductTestDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
