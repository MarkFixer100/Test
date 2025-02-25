

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

       
        public decimal Price { get; set; } = 0;

        public List<Student> Students { get; set; } = [];

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

     }
}
