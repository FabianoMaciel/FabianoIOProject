using FabianoIO.Core.Data;
using FabianoIO.ManagementCourses.Domain;

namespace FabianoIO.Core.Interfaces.Repositories
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetAll();
        Task<IEnumerable<Lesson>> GetByCourseId(Guid courseId);
        void Add(Lesson course);
        Task<bool> CreateProgressLessonByCourse(Guid courseId, Guid studentId);
        Task<bool> StartLesson(Guid lessonId, Guid studentId);
        Task<bool> FinishLesson(Guid lessonId, Guid studentId);
    }
}
