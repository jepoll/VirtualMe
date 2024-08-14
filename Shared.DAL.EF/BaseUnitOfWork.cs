using Microsoft.EntityFrameworkCore;
using Shared.Contracts.DAL;

namespace Shared.DAL.EF;

public abstract class BaseUnitOfWork<TAppDbContext> : IUnitOfWork
    where TAppDbContext : DbContext
{
    protected readonly TAppDbContext UOWDbContext;

    protected BaseUnitOfWork(TAppDbContext dbContext)
    {
        UOWDbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UOWDbContext.SaveChangesAsync();
    }

}