using Claims.Domain.Enums;
using Shared.Core.Domain;

namespace Claims.Domain.Entities;

public class Claim :Entity
{
    public Guid Id { get; set; }
    public Guid CoverId { get; set; }
    public DateTime Created { get; set; }
    public string Name { get; set; }
    public ClaimType Type { get; set; }
    public decimal DamageCost { get; set; }
    public Cover Cover { get; set; } 
}