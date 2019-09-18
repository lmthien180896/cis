namespace CIS.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CISDbContext dbContext;

        public CISDbContext Init()
        {
            return dbContext ?? (dbContext = new CISDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}