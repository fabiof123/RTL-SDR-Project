using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RtlSdrServer.Configuration;
using RtlSdrServer.Web;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaDbContextFactory : IDesignTimeDbContextFactory<LetturaDbContext>
    {
        public LetturaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<LetturaDbContext>();
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSource: true
             );

            builder.UseSqlServer(configuration.GetConnectionString(RtlSdrServerConsts.ConnectionStringName));
            return new LetturaDbContext(builder.Options);
        }
    }
}
