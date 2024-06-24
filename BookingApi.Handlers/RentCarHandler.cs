using BookingApi.Saga.Messages;
using MassTransit;

namespace BookingApi.Handlers;

public class RentCarHandler() : IConsumer<RentCar>
{
    public async Task Consume(ConsumeContext<RentCar> context)
    {
        Console.WriteLine($"Car renting - Plate Number {context.Message.CarPlateNumber} for traveler {context.Message.Email}");

        await context.Publish(new CarRented
        {
            TravelerId = context.Message.TravelerId,
            Email = context.Message.Email,
            CarPlateNumber = context.Message.CarPlateNumber
        });
    }
}