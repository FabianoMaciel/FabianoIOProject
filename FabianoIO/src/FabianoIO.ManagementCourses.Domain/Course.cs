using FabianoIO.Core.DomainObjects;

namespace FabianoIO.ManagementCourses.Domain
{
    public class Course : Entity, IAggregateRoot
    {
        public Course() 
        { 
        }

        public string Name { get; private set; }
        public int TotalHours { get; private set; }
        public string Description { get; private set; }
        private readonly List<Lesson> _lessons;
        public IReadOnlyCollection<Lesson> Lessons => _lessons;

        public void AddLesson(Lesson lesson)
        {
            //TO DO validate if exists and add lesson throw exception
        }

        private bool LessonExistis(Lesson lesson)
        {
            //TO DO validate if exists
            return false;
        }
    }
}
