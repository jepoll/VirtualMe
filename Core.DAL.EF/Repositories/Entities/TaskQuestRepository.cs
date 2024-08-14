using AutoMapper;
using Core.Contracts.DAL.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainE = Core.Domain.Entities;
using DALDTOE = Core.DAL.DTO.Entities;

namespace Core.DAL.EF.Repositories.Entities;

public class TaskQuestRepository : BaseEntityRepository<CoreDomainE.TaskQuest, DALDTOE.TaskQuest, AppDbContext>, ITaskQuestRepository
{
    public TaskQuestRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainE.TaskQuest, DALDTOE.TaskQuest>(mapper))
    {
    }
    public async Task<IEnumerable<DALDTOE.TaskQuest>> GetWithDataAsync()
    {
        return (await CreateQuery().Include("Activity").Include("TaskType").Select(e => Mapper.Map(e)).ToListAsync()!)!;
    }

    public DALDTOE.TaskQuest GetByIdWithData(Guid id)
    {
        return Mapper.Map(CreateQuery().Include("Activity").Include("TaskType").First(e => e.Id.Equals(id)))!;
    }
}