using Core.Contracts.DAL.Repositories.AddressTables;
using Core.Contracts.DAL.Repositories.Entities;
using Shared.Contracts.DAL;

namespace Core.Contracts.BLL.Services.Entities;

public interface ITaskQuestService : IEntityRepository<Core.BLL.DTO.Entities.TaskQuest>, ITaskQuestRepositoryShared<Core.BLL.DTO.Entities.TaskQuest>
{
    
}