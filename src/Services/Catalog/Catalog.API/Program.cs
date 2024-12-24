var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database") ?? string.Empty);
}).UseLightweightSessions();
var app = builder.Build();

app.MapCarter();

app.Run();
