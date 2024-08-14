using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.AddressTables;

public interface IAvatarsTasksService : IEntityRepository<Core.BLL.DTO.AddressTables.AvatarsTasks>, IAvatarsTasksRepositoryShared<Core.BLL.DTO.AddressTables.AvatarsTasks>
{
    
}