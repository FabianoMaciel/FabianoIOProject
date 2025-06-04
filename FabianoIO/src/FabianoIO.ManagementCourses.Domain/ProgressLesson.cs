using FabianoIO.Core.DomainObjects;
using FabianoIO.Core.Enums;

namespace FabianoIO.ManagementCourses.Domain
{
    public class ProgressLesson(Guid studentId, Guid courseId) : Entity, IAggregateRoot
    {
        public Guid StudentId { get; set; } = studentId;
        public Guid CourseId { get; set; } = courseId;
        public EProgressLesson ProgressionStatus { get; private set; }
    }
}
