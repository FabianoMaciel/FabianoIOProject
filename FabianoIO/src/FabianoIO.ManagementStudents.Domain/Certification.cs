using FabianoIO.Core.DomainObjects;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.ManagementStudents.Domain
{
    public class Certification : Entity
    {
        public Guid CourseId { get; set; }
        public Guid StudentId { get; set; }
        public User Student { get; private set; }
    }
}
