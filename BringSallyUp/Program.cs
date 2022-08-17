using Application;
using Application.Commands;
using Application.Persistence;
using Application.Queries;
using Infrastructure.OneOf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

const string MySpecificCors = "_mySpecificCorsBringSallyUp";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationMediatR));

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables() // to get cn from azure
    .Build();
builder.Services.AddDbContext<BimboContext>(options =>
    options.UseSqlServer(config["BigSallyUpCn"], opt =>
    {
        opt.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(15),
            errorNumbersToAdd: new List<int> { 4060 });
    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MySpecificCors,
        policy =>
        {
            if (builder.Environment.IsDevelopment())
            {
                policy
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:8080");
            }
            else
            {
                policy
                    .AllowAnyHeader()
                    .WithOrigins("https://bigsallyup.z6.web.core.windows.net");
            }
        });
});


var app = builder.Build();

app.UseCors(MySpecificCors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/api/login", async (IMediator mediatr, LoginCommand loginCommand) =>
{
    var result = await mediatr.Send(loginCommand);
    if (result.IsSuccess())
    {
        return Results.Ok(new { UserId = result.Value });
    }

    return Results.BadRequest(result.AsError().Message);
})
.WithName("PostLogin");

app.MapPost("/api/attempt", async (IMediator mediatr, AttemptCommand attemptCommand) =>
{
    await mediatr.Send(attemptCommand);
    return Results.Ok();
})
.WithName("PostAttempt");

app.MapGet("/api/attempts", async (HttpRequest request, IMediator mediatr) =>
{
    Guid? userId = null;
    var userIdStringValue = request.Query["UserId"];
    if (userIdStringValue != StringValues.Empty)
    {
        userId = Guid.Parse(userIdStringValue);
    }

    var attempts = await mediatr.Send(new AttemptQuery(userId));
    return Results.Ok(attempts);
})
.WithName("GetAttempt");


app.Run();
