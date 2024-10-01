using Claims.Domain.Enums;
using Shared.Core.Domain;

namespace Claims.Domain.Entities;

public class Cover : Entity
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public CoverType Type { get; set; }
    public decimal Premium { get; set; }
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
}