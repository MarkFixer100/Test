
namespace Domain.Entities
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool Distancelearning {get; set;} 

        public bool IsPaid { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}
