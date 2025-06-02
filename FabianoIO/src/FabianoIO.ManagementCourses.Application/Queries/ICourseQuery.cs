using FabianoIO.ManagementCourses.Application.Queries.ViewModels;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public interface ICourseQuery
    {
        Task<IEnumerable<CourseViewModel>> GetAll();
    }
}
