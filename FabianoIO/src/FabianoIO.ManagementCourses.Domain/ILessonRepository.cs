using FabianoIO.Core.Data;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.Core.Interfaces.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetAll();

        void Add(Lesson course);
    }
}
