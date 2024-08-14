using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.AddressTables;

public interface IAvatarsActivityService : IEntityRepository<Core.BLL.DTO.AddressTables.AvatarsActivity>,
    IAvatarsActivityRepositoryShared<Core.BLL.DTO.AddressTables.AvatarsActivity>
{
    
}