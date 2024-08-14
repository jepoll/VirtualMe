using AutoMapper;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class LogsService : BaseEntityService<Core.DAL.DTO.Entities.Logs,
    Core.BLL.DTO.Entities.Logs, ILogsRepository>, ILogsService
{
    public LogsService(ICoreUnitOfWork uow, ILogsRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Logs, Core.BLL.DTO.Entities.Logs>(mapper))
    {
    }
}