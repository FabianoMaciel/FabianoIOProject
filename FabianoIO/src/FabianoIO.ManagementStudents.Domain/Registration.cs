using FabianoIO.Core.DomainObjects;
using FabianoIO.Core.Enums;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.ManagementStudents.Domain
{
    public class Registration : Entity
    {
        public Guid StudentId { get; set; }
        public Guid LessonId { get; set; }
        public DateTime RegistrationTime { get; set; }
        public EProgressLesson Status { get; set; }
        public User Student { get; set; }

        public Registration(Guid studentId, Guid lessonId, DateTime registrationTime)
        {
            StudentId = studentId;
            LessonId = lessonId;
            RegistrationTime = registrationTime;
            Status = EProgressLesson.NotStarted;
        }
    }
}
