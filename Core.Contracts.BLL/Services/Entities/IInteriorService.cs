using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface IInteriorService : IEntityRepository<Core.BLL.DTO.Entities.Interior>, IInteriorRepositoryShared<Core.BLL.DTO.Entities.Interior>
{
    
}