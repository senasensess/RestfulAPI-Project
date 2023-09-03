using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Infrastructure.SeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                    new Category { Id = 1, Name = "Kasap", Description = "Et ve tavuk ürünleri bulunur!!" } ,
                    new Category { Id = 2, Name = "Manav", Description = "Meyve ve sebzeler bulunur!!" },
                    new Category { Id = 3, Name = "Şarküteri", Description = "Süt ürünleri bulunur!!" }
                );
        }
    }
}
