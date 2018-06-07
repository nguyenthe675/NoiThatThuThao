namespace SHY.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private ShyDbContext dbContext;

        public ShyDbContext Init()
        {
            return dbContext ?? (dbContext = new ShyDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}