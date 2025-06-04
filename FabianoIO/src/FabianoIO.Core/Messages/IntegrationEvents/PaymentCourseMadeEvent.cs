namespace FabianoIO.Core.Messages.IntegrationEvents;

public class PaymentCourseMadeEvent(Guid courseId, Guid studentId) : Event
{
    public Guid CourseId { get; set; } = courseId;
    public Guid StudentId { get; set; } = studentId;
}