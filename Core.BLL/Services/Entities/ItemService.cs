using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class ItemService : BaseEntityService<Core.DAL.DTO.Entities.Item,
    Core.BLL.DTO.Entities.Item, IItemRepository>, IItemService
{
    public ItemService(ICoreUnitOfWork uow, IItemRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Item, Core.BLL.DTO.Entities.Item>(mapper))
    {
    }

    public Item GetItemByIdValue(string id)
    {
        return Mapper.Map(Rep.GetItemByIdValue(id))!;
    }
}