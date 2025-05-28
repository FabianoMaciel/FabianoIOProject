using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementCourses.Domain
{
    public class Lesson(string name, string subject) : Entity, IAggregateRoot
    {
        public string Name { get; set; } = name;
        public string Subject { get; set; } = subject;

        public Guid CourseId { get; set; }
    }
}
