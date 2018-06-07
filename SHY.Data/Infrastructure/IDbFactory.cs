using System;

namespace SHY.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ShyDbContext Init();
    }
}