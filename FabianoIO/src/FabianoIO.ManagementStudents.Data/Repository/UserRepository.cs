using FabianoIO.Core.Data;
using FabianoIO.ManagementStudents.Domain;
using Microsoft.EntityFrameworkCore;

namespace FabianoIO.ManagementStudents.Data.Repository
{
    public class UserRepository(StudentsContext db) : IUserRepository
    {
        private readonly DbSet<User> _dbSet = db.Set<User>();
        public IUnitOfWork UnitOfWork => db;

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await db.SystemUsers.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<IEnumerable<User>> GetStudents()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Add(User user)
        {
            _dbSet.Add(user);
        }
        public void Delete(User User)
        {
            throw new NotImplementedException();
        }

        public void Update(User User)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
