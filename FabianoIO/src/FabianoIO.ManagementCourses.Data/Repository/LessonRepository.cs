using FabianoIO.Core.Data;
using FabianoIO.Core.Enums;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace FabianoIO.ManagementCourses.Data.Repository
{
    public class LessonRepository(CoursesContext _courseContext) : ILessonRepository
    {
        private readonly DbSet<Lesson> _dbSet = _courseContext.Set<Lesson>();
        public IUnitOfWork UnitOfWork => _courseContext;
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

        public async Task<bool> CreateProgressLessonByCourse(Guid courseId, Guid studentId)
        {
            var lessons = await GetByCourseId(courseId);

            foreach (var lesson in lessons)
            {
                _courseContext.Add(new ProgressLesson(lesson.Id, studentId, EProgressLesson.NotStarted));
            }

            await _courseContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StartLesson(Guid lessonId, Guid studentId)
        {
            if (!ExistProgress(lessonId, studentId))
                return false;

            ProgressLesson progressLesson = await _courseContext.ProgressLessons.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);
            progressLesson.ProgressionStatus = EProgressLesson.InProgress;
            _courseContext.ProgressLessons.Update(progressLesson);

            return true;
        }

        public async Task<bool> FinishLesson(Guid lessonId, Guid studentId)
        {
            if (!ExistProgress(lessonId, studentId))
                return false;

            ProgressLesson progressLesson = await _courseContext.ProgressLessons.FirstOrDefaultAsync(a => a.LessonId == lessonId && a.StudentId == studentId);
            progressLesson.ProgressionStatus = EProgressLesson.Completed;
            _courseContext.ProgressLessons.Update(progressLesson);

            return true;
        }

        private bool ExistProgress(Guid lessonId, Guid studentId)
        {
            return _courseContext.ProgressLessons.AsNoTracking().Any(a => a.LessonId == lessonId && a.StudentId == studentId);
        }
    }
}
