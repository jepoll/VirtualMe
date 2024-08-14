namespace Shared.Contracts.DAL;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}