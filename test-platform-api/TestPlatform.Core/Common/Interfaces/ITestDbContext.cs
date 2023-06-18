using Microsoft.EntityFrameworkCore;

namespace TestPlatform.Core.Common.Interfaces;

public interface ITestDbContext
{
    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}