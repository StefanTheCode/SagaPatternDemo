namespace BookingApi.Saga.Messages;

public record BookHotel(string Email, string HotelName, string FlightCode, string CarPlateNumber);

public record BookFlight(Guid TravelerId, string Email, string FlightCode, string CarPlateNumber);

public record RentCar(Guid TravelerId, string Email, string CarPlateNumber);