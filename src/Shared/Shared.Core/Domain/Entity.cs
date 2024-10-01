using Shared.Core.Domain.Interfaces;

namespace Shared.Core.Domain;


public class Entity : IEntity, ISoftDelete
{
    public bool? IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public bool IsActive { get; set; }

    public Entity()
    {
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
        ModifiedAt = null;
    }
}
