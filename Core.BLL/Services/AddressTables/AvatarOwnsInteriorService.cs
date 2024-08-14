using AutoMapper;
using Core.BLL.DTO.AddressTables;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.AddressTables;

public class AvatarOwnsInteriorService : BaseEntityService<Core.DAL.DTO.AddressTables.AvatarOwnsInterior,
    Core.BLL.DTO.AddressTables.AvatarOwnsInterior, IAvatarOwnsInteriorRepository>, IAvatarOwnsInteriorService
{
    public AvatarOwnsInteriorService(ICoreUnitOfWork uow, IAvatarOwnsInteriorRepository repository, IMapper mapper) : base(uow, repository,
        new BllDalMapper<Core.DAL.DTO.AddressTables.AvatarOwnsInterior, Core.BLL.DTO.AddressTables.AvatarOwnsInterior>(mapper))
    {
    }

    public async Task<IEnumerable<AvatarOwnsInterior>> GetAllByAvatarsIdAsync(Guid id)
    {
        return Rep.GetAllByAvatarsId(id).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<AvatarOwnsInterior>> GetAllByInteriorIdAsync(Guid id)
    {
        return Rep.GetAllByInteriorId(id).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<AvatarOwnsInterior>> GetAllByAvatarIdWithInteriorAsync(Guid id)
    {
        return (await Rep.GetAllByAvatarIdWithInteriorAsync(id)).Select(e => Mapper.Map(e))!;
    }
}