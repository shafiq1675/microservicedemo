using MongoRepo.Context;

namespace Catelogue.API.Context
{
    public class CatelogueDBContext : ApplicationDbContext
    {
        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        static string connectionString = configuration.GetConnectionString("Catelogue.API");
        static string databaseName = configuration.GetValue<string>("DatabaseName");
        public CatelogueDBContext() : base(connectionString, databaseName)
        {

        }
    }
}
