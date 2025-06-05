using FabianoIO.Core.Interfaces.Repositories;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;

namespace FabianoIO.ManagementCourses.Application.Queries
{
    public class CourseQuery : ICourseQuery
    {
        private readonly ICourseRepository _courseRepository;

        public CourseQuery(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseViewModel>> GetAll()
        {
            var courses = await _courseRepository.GetAll();

            return courses.Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Price = c.Price,
                //Aulas = c.Aulas.Select(a => new AulaViewModel
                //{
                //    Id = a.Id,
                //    Nome = a.Nome,
                //    Conteudo = a.Conteudo
                //}).ToList()
            }).ToList();
        }

        public async Task<CourseViewModel> GetById(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);

            return new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
            };
        }
    }
}
