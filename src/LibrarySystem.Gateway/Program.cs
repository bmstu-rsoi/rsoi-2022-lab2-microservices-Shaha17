using LibrarySystem.Gateway.Services;
using LibrarySystem.Gateway.Utils.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<LibrariesService>((_) =>
{
    var libraryServiceHost = Environment.GetEnvironmentVariable("LIBRARY_HOST");
    return new LibrariesService(libraryServiceHost);
});
builder.Services.AddTransient<RatingService>((_) =>
{
    var ratingServiceHost = Environment.GetEnvironmentVariable("RATINGS_HOST");
    return new RatingService(ratingServiceHost);
});

builder.Services.AddTransient<ReservationsService>(_ =>
{
    var reservationsServiceHost = Environment.GetEnvironmentVariable("RESERVATIONS_HOST");
    return new ReservationsService(reservationsServiceHost);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();