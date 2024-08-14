using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface IItemService : IEntityRepository<Core.BLL.DTO.Entities.Item>, IItemRepositoryShared<Core.BLL.DTO.Entities.Item>
{
    
}