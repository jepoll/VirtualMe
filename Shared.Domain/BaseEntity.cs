using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Shared.Domain;

public abstract class BaseEntityId : BaseEntityId<Guid>, IDomainEntityId
{
}

public abstract class BaseEntityId<TKey> : IDomainEntityId<TKey> 
    where TKey : IEquatable<TKey>
{
    [Key]
    public TKey Id { get; set; } = default!;
}