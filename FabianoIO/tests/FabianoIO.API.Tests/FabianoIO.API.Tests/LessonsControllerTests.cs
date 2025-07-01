using FabianoIO.Api.Tests.Config;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using System.Net.Http.Json;

namespace FabianoIO.API.Tests
{
    [TestCaseOrderer("FabianoIO.API.Tests.Config.PriorityOrderer", "FabianoIO.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class LessonsControllerTests
    {
        private readonly IntegrationTestsFixture _fixture;

        public LessonsControllerTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddLesson_Success()
        {
            // Arrange
            var courseId = await _fixture.GetIdCourse();
            var lesson = new LessonViewModel
            {
                Name = "Lesson 1",
                CourseId = courseId,
                Subject = "Random",
                TotalHours = 80
            };

            await _fixture.LoginApi();
            _fixture.Client.SetToken(_fixture.Token);

            // Act
            var response = await _fixture.Client.PostAsJsonAsync($"/api/lessons", lesson);

            await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}