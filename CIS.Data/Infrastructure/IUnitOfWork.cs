namespace CIS.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}