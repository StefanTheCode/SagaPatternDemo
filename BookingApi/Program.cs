using BookingApi.Database;
using BookingApi.Saga;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace BookingApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BookingDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDb")));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumers(typeof(Program).Assembly);

            busConfigurator.AddSagaStateMachine<BookingSaga, BookingSagaData>()
                .EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<BookingDbContext>();

                    r.UsePostgres();
                });

            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(builder.Configuration.GetConnectionString("RabbitMq")!), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.UseInMemoryOutbox(context);

                cfg.ConfigureEndpoints(context);
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
