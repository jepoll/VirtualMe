using AutoMapper;
using Core.Contracts.DAL.Repositories.AddressTables;
using Microsoft.EntityFrameworkCore;
using Shared.DAL.EF;
using CoreDomainAT = Core.Domain.AddressTables;
using DALDTOAT = Core.DAL.DTO.AddressTables;

namespace Core.DAL.EF.Repositories.AddressTables;

public class AvatarsTasksRepository : BaseEntityRepository<CoreDomainAT.AvatarsTasks, DALDTOAT.AvatarsTasks, AppDbContext>, IAvatarsTasksRepository
{
    public AvatarsTasksRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DalDomainMapper<CoreDomainAT.AvatarsTasks, DALDTOAT.AvatarsTasks>(mapper))
    {
    }
    //TODO: custom methods
    public async Task<IEnumerable<DALDTOAT.AvatarsTasks>> GetWithDataAsync()
    {
        return (await CreateQuery().Include("TaskQuest").Include("Avatar").ToListAsync()).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<DALDTOAT.AvatarsTasks>> GetByAvatarIdWithDataAsync(Guid id)
    {
        return (await CreateQuery().Where(e => e.AvatarId.Equals(id))
            .Include("TaskQuest")
            .Include("TaskQuest.Activity")
            .Include("TaskQuest.TaskType")
            .Include("Avatar")
            .ToListAsync()).Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Guid>> GetOnlyIdsByAvatarId(Guid id)
    {
        return (await CreateQuery().Where(e => e.AvatarId.Equals(id))
            .Include("TaskQuest")
            .Include("TaskQuest.Activity").ToListAsync()).Select(e => e.TaskQuest.ActivityId).ToList();
    }

    public async Task<IEnumerable<DALDTOAT.AvatarsTasks>> GetWithoutActivitiesByAvatarId(Guid id)
    {
        return (await CreateQuery().Where(e => e.AvatarId.Equals(id))
            .Include("TaskQuest")
            .ToListAsync()).Select(e => Mapper.Map(e))!;
    }
}