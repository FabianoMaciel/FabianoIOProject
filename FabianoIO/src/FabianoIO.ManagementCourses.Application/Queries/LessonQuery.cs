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
                TotalHours = c.TotalHours
                //Aulas = c.Aulas.Select(a => new AulaViewModel
                //{
                //    Id = a.Id,
                //    Nome = a.Nome,
                //    Conteudo = a.Conteudo
                //}).ToList()
            }).ToList();
        }
    }
}
