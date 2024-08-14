using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class TaskTypeRepository : BaseEntityRepository<CoreDomainE.TaskType, DALDTOE.TaskType, AppDbContext>, ITaskTypeRepository
{
    public TaskTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.TaskType, DALDTOE.TaskType>(mapper))
    {
    }
    //TODO: custom methods
}