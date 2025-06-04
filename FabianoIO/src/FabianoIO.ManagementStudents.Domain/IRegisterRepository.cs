using FabianoIO.Core.Data;

namespace FabianoIO.ManagementStudents.Domain
{
    public interface IRegisterRepository : IRepository<User>
    {
        Task<Registration> StartLesson(Guid studentId, Guid lessonId);
        Task<Registration> FinishLesson(Guid studentId, Guid lessonId);
    }
}
