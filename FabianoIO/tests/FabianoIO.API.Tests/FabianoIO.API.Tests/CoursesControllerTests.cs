using FabianoIO.Api.Tests.Config;
using FabianoIO.ManagementCourses.Application.Queries.ViewModels;
using System.Net.Http.Json;

namespace FabianoIO.API.Tests
{
    [TestCaseOrderer("FabianoIO.API.Tests.Config.PriorityOrderer", "FabianoIO.API.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CoursesControllerTests
    {
        private readonly IntegrationTestsFixture _fixture;

        public CoursesControllerTests(IntegrationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task AddCourse_Success()
        {
            // Arrange
            var course = new CourseViewModel
            {
                Name = "Lesson 1",
                Id = Guid.NewGuid(),
                Description = "Random",
                Price = 800
            };

            await _fixture.LoginApi();
            _fixture.Client.SetToken(_fixture.Token);

            // Act
            var response = await _fixture.Client.PostAsJsonAsync($"/api/courses/create", course);

            await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}