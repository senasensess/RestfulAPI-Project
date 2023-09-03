using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.SeedData
{
    public class AppUserSeedData : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData
                (
                    new AppUser { Id = 1, UserName = "sinaemre", Password = "123" },
                    new AppUser { Id = 2, UserName = "sinaemre2", Password = "123" },
                    new AppUser { Id = 3, UserName = "sinaemre3", Password = "123" }
                );
        }
    }
}
