namespace Shared.Core.Domain.Interfaces;

public interface IEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }

}