using BookingApi.Saga.Messages;
using MassTransit;

namespace BookingApi.Handlers;

public class BookingCompletedHandler() : IConsumer<BookingCompleted>
{
    public Task Consume(ConsumeContext<BookingCompleted> context)
    {
        Console.WriteLine($"Booking process is completed for Traveler {context.Message.TravelerId} with email {context.Message.Email}");

        return Task.CompletedTask;
    }
}