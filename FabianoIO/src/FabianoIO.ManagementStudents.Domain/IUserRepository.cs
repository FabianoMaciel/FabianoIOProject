using FabianoIO.Core.Data;

namespace FabianoIO.ManagementStudents.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetStudents();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetById(Guid id);
        void Add(User user);
        Task<User> GetByEmail(string email);
    }
}
