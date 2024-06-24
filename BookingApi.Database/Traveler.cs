namespace BookingApi.Database;

public class Traveler
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public DateTime BookedOn { get; set; }
}