using FabianoIO.Core.Enums;
using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public class LessonQuery : ILessonQuery
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonQuery(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<IEnumerable<LessonViewModel>> GetAll()
        {
            var lessons = await _lessonRepository.GetAll();

            return lessons.Select(c => new LessonViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                TotalHours = c.TotalHours,
                CourseId = c.CourseId
            }).ToList();
        }

        public async Task<IEnumerable<LessonViewModel>> GetByCourseId(Guid courseId)
        {
            var lessons = await _lessonRepository.GetByCourseId(courseId);

            return lessons.Select(c => new LessonViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                TotalHours = c.TotalHours,
                CourseId = c.CourseId
            }).ToList();
        }

        public bool ExistsProgress(Guid lessonId, Guid studentId)
        {
            return _lessonRepository.ExistProgress(lessonId, studentId);
        }

        public EProgressLesson GetProgressStatusLesson(Guid lessonId, Guid studentId)
        {
            var status = _lessonRepository.GetProgressStatusLesson(lessonId, studentId);

            return status;
        }
    }
}
