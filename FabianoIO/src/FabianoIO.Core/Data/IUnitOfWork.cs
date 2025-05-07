namespace FabianoIO.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
