using System.Text.Json.Serialization;
using CareTrack.Server.Models;

namespace CareTrack.Server.Entities;
public class Medicine : IMedicine
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateOnly ExpirationDate { get; set; } 
}
