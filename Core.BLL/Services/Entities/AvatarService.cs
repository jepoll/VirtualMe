using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class AvatarService : BaseEntityService<Core.DAL.DTO.Entities.Avatar,
    Core.BLL.DTO.Entities.Avatar, IAvatarRepository>, IAvatarService
{
    public AvatarService(ICoreUnitOfWork uow, IAvatarRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Avatar, Core.BLL.DTO.Entities.Avatar>(mapper))
    {
    }

    public IEnumerable<Avatar> GetAvatarsWithUsers()
    {
        return Rep.GetAvatarsWithUsers().Select(e => Mapper.Map(e))!;
    }

    public async Task<IEnumerable<Avatar>> GetAvatarsWithUsersAsync()
    {
        return (await Rep.GetAvatarsWithUsersAsync()).Select(e => Mapper.Map(e))!;
    }

    public Avatar? GetById(Guid id)
    {
        return Mapper.Map(Rep.GetById(id))!;
    }

    public async Task<IEnumerable<Avatar>> GetByUserId(Guid id)
    {
        return (await Rep.GetByUserId(id)).Select(e => Mapper.Map(e))!;
    }

    public async Task<Avatar?> GetAvatarUpdatedByUserId(Guid id)
    {
        var avatar = Mapper.Map((await Rep.GetAvatarUpdatedByUserId(id)));
        if (avatar == null) return avatar;
        var timePassed = DateTime.UtcNow - avatar!.LastChanges;
        
        // Hunger: From 100 to 0 in 50 hours(if stress level is 0) 1 hour - 2 points
        // If stress is 100 hunger speed is twice as much(in 25 hours from 100 to 0) 1 hour - 4 points
        // Stress: Stress increases when completing tasks.
        // Health: If hunger is 0 health reduces. Health from 100 to 0 in 5 hours. 1 hour - 20 points
        // Health: If hunger is 0 health reduces. Health from 0 to 100 in 10 hours. 1 hour - 10 points
        // Stamina: From 0 to 100 in 4. 1 hour - 25 points
        
        
        double? hungerPoints;
        
        if (avatar.Stress == 0)
        {
            hungerPoints = timePassed.TotalHours * 2;
        }
        else
        {
            hungerPoints = timePassed.TotalHours * 2 * (1 + avatar.Stress / 100);
        }

        avatar.Hunger -= (int) hungerPoints;
        if (avatar.Hunger < 0)
            avatar.Hunger = 0;

        
        double? staminaPoints = timePassed.TotalHours * 25;
        avatar.Stamina += (int) staminaPoints;
        if (avatar.Stamina > 100)
            avatar.Stamina = 100;
        
        
        double? healthPoints;
        if (avatar.Hunger == 0)
        {
            healthPoints = timePassed.TotalHours * 20;
            avatar.Health -= (int) healthPoints;
        }
        else
        {
            healthPoints = timePassed.TotalHours * 10;
            avatar.Health += (int) healthPoints;
            
        }

        if (avatar.Health > 100)
            avatar.Health = 100;
        else if (avatar.Health < 0)
        {
            avatar.Health = 0;
            avatar.IsActive = false;
        }

        while (avatar.Exp >= avatar.ExpToLevelUp)
        {
            avatar.Exp -= avatar.ExpToLevelUp;
            avatar.Level++;
            avatar.ExpToLevelUp = (int)(avatar.ExpToLevelUp * 1.25);
        }
        
        avatar.LastChanges = DateTime.Now;

        return avatar;
    }
}