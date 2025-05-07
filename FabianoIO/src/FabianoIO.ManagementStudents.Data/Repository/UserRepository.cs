using FabianoIO.Core.Data;
using FabianoIO.ManagementStudents.Domain;

namespace FabianoIO.ManagementStudents.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void Delete(User User)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<User> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(User User)
        {
            throw new NotImplementedException();
        }
    }
}
