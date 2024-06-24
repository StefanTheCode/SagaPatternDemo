using MassTransit;

namespace BookingApi.Saga;

public class BookingSagaData : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = string.Empty;
    public Guid TravelerId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string HotelName { get; set; } = string.Empty;
    public string FlightCode { get; set; } = string.Empty;
    public string CarPlateNumber { get; set; } = string.Empty;  
 
    public bool HotelBooked { get; set; }
    public bool FlightBooked { get; set; }
    public bool CarRented { get; set; }
    public bool BookingFinished { get; set; }
}