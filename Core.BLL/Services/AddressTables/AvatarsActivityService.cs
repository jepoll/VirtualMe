using AutoMapper;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.EF;
using Core.Domain.AddressTables;
using Shared.BLL;
using AvatarsActivity = Core.BLL.DTO.AddressTables.AvatarsActivity;

namespace Core.BLL.Services.AddressTables;

public class AvatarsActivityService : BaseEntityService<Core.DAL.DTO.AddressTables.AvatarsActivity,
    Core.BLL.DTO.AddressTables.AvatarsActivity, IAvatarsActivityRepository>, IAvatarsActivityService
{
    public AvatarsActivityService(ICoreUnitOfWork uow, IAvatarsActivityRepository repository, IMapper mapper) : base(uow, repository,
        new BllDalMapper<Core.DAL.DTO.AddressTables.AvatarsActivity, Core.BLL.DTO.AddressTables.AvatarsActivity>(mapper))
    {
    }

    public IEnumerable<Core.BLL.DTO.AddressTables.AvatarsActivity> GetWithData()
    {
        return Rep.GetWithData().Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Core.BLL.DTO.AddressTables.AvatarsActivity>> GetWithDataAsync()
    {
        return (await Rep.GetWithDataAsync()).Select(e => Mapper.Map(e))!;
    }

    public AvatarsActivity GetByAvatarId(Guid id)
    {
        return Mapper.Map(Rep.GetByAvatarId(id))!;
    }
}