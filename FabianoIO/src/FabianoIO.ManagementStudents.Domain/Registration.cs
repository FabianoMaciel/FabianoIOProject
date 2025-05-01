using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementStudents.Domain
{
    public class Registration : Entity
    {
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime DateRegistration { get; private set; }
        public int Status { get; private set; }

        public Registration(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
