using FabianoIO.Core.Enums;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public class LessonProgressViewModel(string lessonName, string progressLesson)
    {
        public string LessonName { get; } = lessonName;
        public string ProgressLesson { get; } = progressLesson;
    }
}