using FabianoIO.Core.Data;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.Core.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAll();

        void Add(Course course);
    }
}
