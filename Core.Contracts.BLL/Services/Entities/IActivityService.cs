using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface IActivityService : IEntityRepository<Core.BLL.DTO.Entities.Activity>, IActivityRepositoryShared<Core.BLL.DTO.Entities.Activity>
{
    
}