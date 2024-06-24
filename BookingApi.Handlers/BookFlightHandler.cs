using BookingApi.Saga.Messages;
using MassTransit;

namespace BookingApi.Handlers;

public class BookFlightHandler() : IConsumer<BookFlight>
{
    public async Task Consume(ConsumeContext<BookFlight> context)
    {
        Console.WriteLine($"Booking flight number {context.Message.FlightCode} for traveler {context.Message.Email}");

        await context.Publish(new FlightBooked
        {
            TravelerId = context.Message.TravelerId,
            FlightCode = context.Message.FlightCode,
            CarPlateNumber = context.Message.CarPlateNumber
        });
    }
}