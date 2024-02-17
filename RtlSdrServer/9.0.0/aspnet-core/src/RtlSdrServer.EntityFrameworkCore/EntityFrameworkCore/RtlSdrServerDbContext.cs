using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using RtlSdrServer.Authorization.Roles;
using RtlSdrServer.Authorization.Users;
using RtlSdrServer.MultiTenancy;

namespace RtlSdrServer.EntityFrameworkCore
{
    public class RtlSdrServerDbContext : AbpZeroDbContext<Tenant, Role, User, RtlSdrServerDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public RtlSdrServerDbContext(DbContextOptions<RtlSdrServerDbContext> options)
            : base(options)
        {
        }
    }
}
