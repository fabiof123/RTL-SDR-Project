using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace RtlSdrServer.EntityFrameworkCore
{
    public static class RtlSdrServerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<RtlSdrServerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<RtlSdrServerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
