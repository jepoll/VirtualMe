using AutoMapper;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.DTO.AddressTables;
using Core.DAL.EF;
using Shared.BLL;
using Shared.Contracts.BLL;
using AvatarsTasks = Core.BLL.DTO.AddressTables.AvatarsTasks;

namespace Core.BLL.Services.AddressTables;

public class AvatarsTasksService : BaseEntityService<Core.DAL.DTO.AddressTables.AvatarsTasks,
    Core.BLL.DTO.AddressTables.AvatarsTasks, IAvatarsTasksRepository>, IAvatarsTasksService
{
    public AvatarsTasksService(ICoreUnitOfWork uow, IAvatarsTasksRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.AddressTables.AvatarsTasks, Core.BLL.DTO.AddressTables.AvatarsTasks>(mapper))
    {
    }

    public async Task<IEnumerable<AvatarsTasks>> GetWithDataAsync()
    {
        return (await Rep.GetWithDataAsync()).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<AvatarsTasks>> GetByAvatarIdWithDataAsync(Guid id)
    {
        return (await Rep.GetByAvatarIdWithDataAsync(id)).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Guid>> GetOnlyIdsByAvatarId(Guid id)
    {
        return await Rep.GetOnlyIdsByAvatarId(id);
    }

    public async Task<IEnumerable<AvatarsTasks>> GetWithoutActivitiesByAvatarId(Guid id)
    {
        return (await Rep.GetWithoutActivitiesByAvatarId(id)).Select(e => Mapper.Map(e))!;
    }
}