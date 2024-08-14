using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;
using Shared.DAL.EF;

namespace Core.DAL.EF.Repositories.Entities;

public class LogsRepository : BaseEntityRepository<CoreDomainE.Logs, DALDTOE.Logs, AppDbContext>, ILogsRepository
{
    public LogsRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.Logs, DALDTOE.Logs>(mapper))
    {
    }
    //TODO: custom methods
}