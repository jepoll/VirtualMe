using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class ItemRepository : BaseEntityRepository<CoreDomainE.Item, DALDTOE.Item, AppDbContext>, IItemRepository
{
    public ItemRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Item, DALDTOE.Item>(mapper))
    {
    }

    public DALDTOE.Item GetItemByIdValue(string id)
    {
        return Mapper.Map(CreateQuery().FirstOrDefault(e => e.Id.ToString().Equals(id)))!;
    }
}