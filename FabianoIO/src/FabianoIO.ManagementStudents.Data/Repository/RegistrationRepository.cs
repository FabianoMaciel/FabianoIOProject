using FabianoIO.Core.Data;
using FabianoIO.Core.Enums;
using FabianoIO.ManagementCourses.Domain;
using FabianoIO.ManagementStudents.Domain;
using Microsoft.EntityFrameworkCore;

namespace FabianoIO.ManagementStudents.Data.Repository
{
    public class RegistrationRepository(StudentsContext db) : IRegisterRepository
    {
        private readonly DbSet<Registration> _dbSet = db.Set<Registration>();
        public IUnitOfWork UnitOfWork => db;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<Registration> FinishLesson(Guid studentId, Guid lessonId)
        {
            var lesson = await _dbSet.FirstOrDefaultAsync(a => a.StudentId == studentId && a.LessonId == lessonId);
            if (lesson != null)
                lesson.Status = EProgressLesson.Completed;

            return lesson;
        }

        public async Task<Registration> StartLesson(Guid studentId, Guid lessonId)
        {
            var lesson = await _dbSet.FirstOrDefaultAsync(a => a.StudentId == studentId && a.LessonId == lessonId);
            if (lesson != null)
                lesson.Status = EProgressLesson.InProgress;

            return lesson;
        }
    }
}
