using System;

namespace CIS.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CISDbContext Init();
    }
}