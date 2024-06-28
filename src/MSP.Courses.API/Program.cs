using MSP.Courses.API.Configuration;
using MSP.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .ConfigureServices(builder.Configuration)
    .ConfigureSettings(builder.Configuration)
    .ConfigureInfrastructure(builder.Configuration)
    .ConfigureSwagger()
    //.ConfigureAuthentication(builder.Configuration)
    //.ConfigureAuthorization()
    .ConfigureCoreServices();
    //.ConfigureMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.ConfigureApplication();
app.Run();