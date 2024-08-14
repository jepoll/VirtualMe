using AutoMapper;
using Core.BLL.DTO.AddressTables;
using Core.Contracts.BLL.Services.AddressTables;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.AddressTables;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.AddressTables;

public class RewardService : BaseEntityService<Core.DAL.DTO.AddressTables.Reward,
    Core.BLL.DTO.AddressTables.Reward, IRewardRepository>, IRewardService
{
    public RewardService(ICoreUnitOfWork uow, IRewardRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.AddressTables.Reward, Core.BLL.DTO.AddressTables.Reward>(mapper))
    {
    }

    public async Task<IEnumerable<Reward>> GetWithDataAsync()
    {
        return (await Rep.GetWithDataAsync()).Select(e => Mapper.Map(e))!;
    }

    public Reward GetByTaskQuestId(Guid id)
    {
        return Mapper.Map(Rep.GetByTaskQuestId(id))!;
    }
}