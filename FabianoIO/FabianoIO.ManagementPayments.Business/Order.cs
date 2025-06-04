namespace FabianoIO.ManagementPayments.Business;

public class Order
{
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public double Total { get; set; }
}