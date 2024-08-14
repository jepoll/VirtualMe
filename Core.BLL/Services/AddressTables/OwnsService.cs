using AutoMapper;
using Core.BLL.DTO.AddressTables;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.AddressTables;

public class OwnsService : BaseEntityService<Core.DAL.DTO.AddressTables.Owns,
    Core.BLL.DTO.AddressTables.Owns, IOwnsRepository>, IOwnsService
{
    public OwnsService(ICoreUnitOfWork uow, IOwnsRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.AddressTables.Owns, Core.BLL.DTO.AddressTables.Owns>(mapper))
    {
    }

    public IEnumerable<Owns> GetAllByAvatarId(Guid avatarId)
    {
        return Rep.GetAllByAvatarId(avatarId).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Owns>> GetAllByAvatarIdAsync(Guid avatarId)
    {
        return (await Rep.GetAllByAvatarIdAsync(avatarId)).Select(e => Mapper.Map(e))!;
    }

    public Owns GetByAvatarAndItemIds(Guid avatarId, Guid itemId)
    {
        return Mapper.Map(Rep.GetByAvatarAndItemIds(avatarId, itemId))!;
    }

    public Owns GetByIdString(string id)
    {
        return Mapper.Map(Rep.GetByIdString(id))!;
    }

    public async Task<IEnumerable<Owns>> GetAllWIthDataAsync()
    {
        return (await Rep.GetAllWIthDataAsync()).Select(e => Mapper.Map(e)).ToList()!;
    }

    public async Task<Owns> GetByIdWithData(Guid id)
    {
        return Mapper.Map(await Rep.GetByIdWithData(id))!;
    }
}