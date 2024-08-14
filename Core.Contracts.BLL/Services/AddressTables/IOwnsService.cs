using Core.Contracts.DAL.Repositories.AddressTables;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.AddressTables;

public interface IOwnsService : IEntityRepository<Core.BLL.DTO.AddressTables.Owns>, IOwnsRepositoryShared<Core.BLL.DTO.AddressTables.Owns>
{
    
}