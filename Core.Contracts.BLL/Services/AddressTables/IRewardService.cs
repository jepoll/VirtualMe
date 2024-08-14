using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.AddressTables;

public interface IRewardService : IEntityRepository<Core.BLL.DTO.AddressTables.Reward>, IRewardRepositoryShared<Core.BLL.DTO.AddressTables.Reward>
{
    
}