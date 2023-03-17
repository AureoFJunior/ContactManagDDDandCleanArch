namespace ContactManag.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}