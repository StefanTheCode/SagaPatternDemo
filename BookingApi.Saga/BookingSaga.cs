using BookingApi.Saga.Messages;
using MassTransit;

namespace BookingApi.Saga;
public class BookingSaga : MassTransitStateMachine<BookingSagaData>
{
    public State HotelBooking { get; set; }
    public State FlightBooking { get; set; }
    public State CarRenting { get; set; }
    public State BookingCompleting { get; set; }

    public Event<HotelBooked> HotelBooked { get; set; }
    public Event<FlightBooked> FlightBooked { get; set; }
    public Event<CarRented> CarRented { get; set; }
    public Event<BookingCompleted> BookingCompleted { get; set; }

    public BookingSaga()
    {
        InstanceState(x => x.CurrentState);

        Event(() => HotelBooked, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => FlightBooked, e => e.CorrelateById(m => m.Message.TravelerId));
        Event(() => CarRented, e => e.CorrelateById(m => m.Message.TravelerId));

        Initially(
            When(HotelBooked)
                .Then(context =>
                {
                    context.Saga.TravelerId = context.Message.TravelerId;
                    context.Saga.HotelName = context.Message.HotelName;
                    context.Saga.FlightCode = context.Message.FlightCode;
                    context.Saga.CarPlateNumber = context.Message.CarPlateNumber;
                })
                .TransitionTo(FlightBooking)
                .Publish(context => new BookFlight(context.Message.TravelerId, context.Message.Email, context.Message.FlightCode, context.Message.CarPlateNumber)));

        During(FlightBooking,
            When(FlightBooked)
                .Then(context => context.Saga.FlightBooked = true)
                .TransitionTo(CarRenting)
                .Publish(context => new RentCar(context.Message.TravelerId, context.Message.Email, context.Message.CarPlateNumber)));

        During(CarRenting,
            When(CarRented)
                .Then(context =>
                {
                    context.Saga.CarRented = true;
                    context.Saga.BookingFinished = true;
                })
                .TransitionTo(BookingCompleting)
                .Publish(context => new BookingCompleted
                {
                    TravelerId = context.Message.TravelerId,
                    Email = context.Message.Email
                })
                .Finalize());
    }
}