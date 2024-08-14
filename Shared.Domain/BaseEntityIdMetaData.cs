using System.ComponentModel.DataAnnotations;
using Shared.Contracts.Domain;

namespace Shared.Domain;

public abstract class BaseEntityIdMetaData : BaseEntityIdMetaData<Guid>
{
    
}

public abstract class BaseEntityIdMetaData<Tkey> : BaseEntityId<Tkey>, IDomainEntityMetaData
    where Tkey : IEquatable<Tkey>
{
    [MaxLength(128)]
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    [MaxLength(128)]
    public string UpdatedBy { get; set; } = default!;
    public DateTime UpdatedAt { get; set; }
}