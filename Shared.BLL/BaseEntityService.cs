using System.Drawing;
using Core.Contracts.DAL;
using Shared.Contracts.BLL;
using Shared.Contracts.DAL;
using Shared.Contracts.Domain;

namespace Shared.BLL;

public class BaseEntityService <TDalEntity, TBllEntity, TRepository> :
    BaseEntityService<TDalEntity, TBllEntity, TRepository, Guid>,
    IEntityService<TBllEntity>
    where TBllEntity : class, IDomainEntityId
    where TRepository : IEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId<Guid>
{
    public BaseEntityService(ICoreUnitOfWork uow, TRepository repository, IBLLMapper<TDalEntity, TBllEntity> mapper) : base(uow, repository, mapper)
    {
    }
}

public class BaseEntityService<TDalEntity, TBllEntity, TRepository, TKey> : IEntityService<TBllEntity, TKey>
    where TRepository : IEntityRepository<TDalEntity, TKey>
    where TKey : IEquatable<TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
{
    private readonly ICoreUnitOfWork Uow;
    protected readonly TRepository Rep;
    protected readonly IBLLMapper<TDalEntity, TBllEntity> Mapper;

    public BaseEntityService(ICoreUnitOfWork uow, TRepository rep, IBLLMapper<TDalEntity, TBllEntity> mapper)
    {
        Uow = uow;
        Rep = rep;
        Mapper = mapper;
    }

    public TBllEntity Add(TBllEntity entity)
    {
        return Mapper.Map(Rep.Add(Mapper.Map(entity)!))!;
    }

    public TBllEntity Update(TBllEntity entity)
    {
        return Mapper.Map(Rep.Update(Mapper.Map(entity)!))!;
    }

    public int Remove(TBllEntity entity, TKey? userId = default)
    {
        return Rep.Remove(Mapper.Map(entity), userId);
    }

    public int Remove(TKey id, TKey? userId = default)
    {
        return Rep.Remove(id, userId);
    }

    public TBllEntity? FirstOrDefault(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(Rep.FirstOrDefault(id, userId, noTracking));
    }

    public IEnumerable<TBllEntity> GetAll(TKey? userId = default, bool noTracking = true)
    {
        return Rep.GetAll(userId, noTracking).Select(e => Mapper.Map(e));
    }

    public bool Exists(TKey id, TKey? userId = default)
    {
        return Rep.GetAll().Any(e => e.Id.Equals(id));
    }

    public async Task<TBllEntity?> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Rep.FirstOrDefaultAsync(id, userId, noTracking));
    }

    public async Task<IEnumerable<TBllEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
    {
        return (await Rep.GetAllAsync(userId, noTracking)).Select(e => Mapper.Map(e));
    }

    public async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
    {
        return (await Rep.GetAllAsync()).Any(e => e.Id.Equals(id));
    }

    public async Task<int> RemoveAsync(TBllEntity entity, TKey? userId = default)
    {
        return await Rep.RemoveAsync(Mapper.Map(entity), userId);
    }

    public async Task<int> RemoveAsync(TKey id, TKey? userId = default)
    {
        return await Rep.RemoveAsync(id, userId);
    }
}