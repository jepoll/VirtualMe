using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface IActivityTypeService : IEntityRepository<Core.BLL.DTO.Entities.ActivityType>, IActivityTypeRepositoryShared<Core.BLL.DTO.Entities.ActivityType>
{
    
}