
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infostructure.Data
{
    public class Config : IEntityTypeConfiguration<Course>
    {
  

        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    Name = "Web Design",
                    Description = "Разработка дизайна",
                    Price = 5000,
                    Created = new DateTime(2025, 2, 23),
                    Updated = new DateTime(2025, 2, 23)
                }

                );
        }
    }
}
