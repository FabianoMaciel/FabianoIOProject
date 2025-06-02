using FabianoIO.ManagementCourses.Application.Queries.ViewModels;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public interface ILessonQuery
    {
        Task<IEnumerable<LessonViewModel>> GetAll();
    }
}
