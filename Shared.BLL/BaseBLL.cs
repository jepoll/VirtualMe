using Shared.Contracts.BLL;
using Shared.Contracts.DAL;
using Shared.DAL.EF;
using Microsoft.EntityFrameworkCore;


namespace Shared.BLL;


public abstract class BaseBLL<TAppDbContext> : IBLL
    where TAppDbContext : DbContext
{
    protected readonly IUnitOfWork Uow;

    protected BaseBLL(IUnitOfWork uow)
    {
        Uow = uow;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Uow.SaveChangesAsync();
    }
}
