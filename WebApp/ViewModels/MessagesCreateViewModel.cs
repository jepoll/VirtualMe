using Core.Domain.Entities;
using Message = Core.BLL.DTO.Entities.Message;

namespace WebApp.ViewModels;

public class MessagesCreateViewModel
{
    public Message Message { get; set; } = default!;
    public IEnumerable<Message> Messages = default!;
    public Guid ChatId { get; set; } = default!;
    public string UserName { get; set; } = default!;
}