using FabianoIO.Core.Data;

namespace FabianoIO.ManagementStudents.Domain
{
    public interface IRegistrationRepository : IRepository<User>
    {
        Task<Registration> FinishCourse(Guid studentId, Guid courseId);
        Registration AddRegistration(Guid studentId, Guid courseId);
    }
}
