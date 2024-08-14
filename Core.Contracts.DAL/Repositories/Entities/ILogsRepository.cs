using DALDTOE = Core.DAL.DTO.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.DAL.Repositories.Entities;

public interface ILogsRepository : IEntityRepository<DALDTOE.Logs>
{
    //TODO: custom methods
}