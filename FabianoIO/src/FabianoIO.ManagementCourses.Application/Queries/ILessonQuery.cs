using FabianoIO.Core.Enums;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public interface ILessonQuery
    {
        Task<IEnumerable<LessonViewModel>> GetAll();
        Task<IEnumerable<LessonViewModel>> GetByCourseId(Guid courseId);
        EProgressLesson GetProgressStatusLesson(Guid lessonId, Guid studentId);
        bool ExistsProgress(Guid lessonId, Guid studentId);
    }
}
