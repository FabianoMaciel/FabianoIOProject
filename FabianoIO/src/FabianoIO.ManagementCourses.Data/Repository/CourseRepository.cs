using FabianoIO.Core.Data;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FabianoIO.ManagementCourses.Data.Repository
{
    public class CourseRepository(CourseContext courseContext) : ICourseRepository
    {
        private readonly DbSet<Course> _dbSet = courseContext.Set<Course>();
        public IUnitOfWork UnitOfWork => courseContext;
        public void Add(Course course)
        {
            _dbSet.Add(course);
        }

        public Task Delete(Course entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public Task<IEnumerable<Course>> GetAll(string includes = null, Expression<Func<Course, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task<Course> GetById(Guid id, string includes = null, Expression<Func<Course, bool>> expression = null)
        {
            throw new NotImplementedException();
        }

        public Task Update(Course entity)
        {
            throw new NotImplementedException();
        }
    }
}
