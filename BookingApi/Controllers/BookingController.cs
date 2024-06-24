using BookingApi.Saga.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;
[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IBus _bus;

    public BookingController(ILogger<BookingController> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    [HttpPost("bookTrip")]
    public IActionResult BookTrip(BookingDetails bookingDetails)
    {
        _bus.Publish(new BookHotel(bookingDetails.Email, bookingDetails.HotelName, bookingDetails.FlightCode, bookingDetails.CarPlateNumber));

        return Accepted();
    }

    public class BookingDetails
    {
        public string Email { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public string FlightCode { get; set; } = string.Empty;
        public string CarPlateNumber { get; set; } = string.Empty;
    }
}