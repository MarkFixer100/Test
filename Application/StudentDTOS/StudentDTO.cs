using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentDTOS
{
    public class StudentDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public bool Distancelearning { get; set; }

        public bool IsPaid { get; set; }

        public Guid CourseId { get; set; }

    }
}
