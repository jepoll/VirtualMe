using AutoMapper;
using Core.BLL.DTO.Entities;
using Core.Contracts.BLL.Services.Entities;
using Core.Contracts.DAL;
using Core.Contracts.DAL.Repositories.Entities;
using Core.DAL.EF;
using Core.DAL.EF.Repositories.Entities;
using NuGet.Protocol.Core.Types;
using Shared.BLL;

namespace Core.BLL.Services.Entities;

public class MessageService : BaseEntityService<Core.DAL.DTO.Entities.Message,
    Core.BLL.DTO.Entities.Message, IMessageRepository>, IMessageService
{
    public MessageService(ICoreUnitOfWork uow, IMessageRepository repository, IMapper mapper) : base(uow, repository, 
        new BllDalMapper<Core.DAL.DTO.Entities.Message, Core.BLL.DTO.Entities.Message>(mapper))
    {
    }

    public IEnumerable<Message> GetAllMessages(Guid chatId)
    {
        return Rep.GetAllMessages(chatId).Select(e => Mapper.Map(e))!;
    }
}