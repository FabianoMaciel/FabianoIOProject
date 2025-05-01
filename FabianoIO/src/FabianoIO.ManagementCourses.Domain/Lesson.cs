using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementCourses.Domain
{
    public class Lesson(string name, string subject) : Entity, IAggregateRoot
    {
        public string Name { get; private set; } = name;
        public string Subject { get; private set; } = subject;

        public Guid CourseId { get; private set; }
    }
}
