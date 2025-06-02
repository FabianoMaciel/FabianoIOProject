using FabianoIO.Core.DomainObjects;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.ManagementStudents.Domain
{
    public class Registration : Entity
    {
        public Guid StudentId { get; private set; }
        public Guid CourseId { get; private set; }
        public DateTime RegistrationTime { get; private set; }
        public RegistrationStatus Status { get; private set; }
        public User Student { get; private set; }

        public Registration(Guid studentId, Guid courseId, DateTime registrationTime)
        {
            StudentId = studentId;
            CourseId = courseId;
            RegistrationTime = registrationTime;
            Status = RegistrationStatus.Active;
        }
    }
}
