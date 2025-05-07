using FabianoIO.Core.Data;

namespace FabianoIO.ManagementStudents.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetStudents();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> ObterPorId(Guid id);
        void Add(User user);
        void Update(User User);
        void Delete(User User);
    }
}
