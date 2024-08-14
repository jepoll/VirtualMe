using AutoMapper;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class ChatService : BaseEntityService<Core.DAL.DTO.Entities.Chat,
    Core.BLL.DTO.Entities.Chat, IChatRepository>, IChatService
{
    public ChatService(ICoreUnitOfWork uow, IChatRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Chat, Core.BLL.DTO.Entities.Chat>(mapper))
    {
    }
}