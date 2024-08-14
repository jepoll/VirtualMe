using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface IAvatarService : IEntityRepository<Core.BLL.DTO.Entities.Avatar>, IAvatarRepositoryShared<Core.BLL.DTO.Entities.Avatar>
{
    
}
