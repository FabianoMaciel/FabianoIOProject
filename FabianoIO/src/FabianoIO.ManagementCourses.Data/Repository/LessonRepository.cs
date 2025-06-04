using FabianoIO.Core.Data;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FabianoIO.ManagementCourses.Data.Repository
{
    public class LessonRepository(CoursesContext courseContext) : ILessonRepository
    {
        private readonly DbSet<Lesson> _dbSet = courseContext.Set<Lesson>();
        public IUnitOfWork UnitOfWork => courseContext;
        public void Add(Lesson lesson)
        {
            _dbSet.Add(lesson);
        }

        public Task Delete(Lesson entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Lesson>> GetByCourseId(Guid courseId)
        {
            return await _dbSet.Where(a => a.CourseId == courseId).AsNoTracking().ToListAsync();
        }
    }
}
