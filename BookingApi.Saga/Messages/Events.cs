namespace BookingApi.Saga.Messages;

public class HotelBooked
{
    public Guid TravelerId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
    public string FlightCode { get; set; } = string.Empty;
    public string CarPlateNumber { get; set; } = string.Empty;
}

public class FlightBooked
{
    public Guid TravelerId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FlightCode { get; set; } = string.Empty;
    public string CarPlateNumber { get; set; } = string.Empty;
}

public class CarRented
{
    public Guid TravelerId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string CarPlateNumber { get; set; } = string.Empty;
}

public class BookingCompleted
{
    public Guid TravelerId { get; set; }
    public string Email { get; set; } = string.Empty;
}