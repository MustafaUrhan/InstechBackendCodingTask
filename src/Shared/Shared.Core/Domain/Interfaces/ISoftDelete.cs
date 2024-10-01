namespace Shared.Core.Domain.Interfaces;

public interface ISoftDelete
{
    public bool? IsDeleted { get; set; }
    public bool IsActive { get; set; }
}