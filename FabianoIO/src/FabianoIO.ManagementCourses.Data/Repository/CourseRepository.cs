using FabianoIO.Core.Data;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FabianoIO.ManagementCourses.Data.Repository
{
    public class CourseRepository(CoursesContext courseContext) : ICourseRepository
    {
        private readonly DbSet<Course> _dbSet = courseContext.Set<Course>();
        public IUnitOfWork UnitOfWork => courseContext;
        public void Add(Course course)
        {
            _dbSet.Add(course);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
    }
}
