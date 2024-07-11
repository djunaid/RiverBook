using RiverBooks.Books;
using FastEndpoints;
using RiverBooks.Users;
using Serilog;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Reflection;

var logger = Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

logger.Information("Starting web host");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog( (_, config) => 
            config.ReadFrom.Configuration(builder.Configuration));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

// add service dependency for modules
builder.Services.AddFastEndpoints()
    .AddAuthenticationJWTBearer(builder.Configuration["Auth:JwtSecret"])
    .AddAuthorization()
    .SwaggerDocument();

builder.Services.AddBookService(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserServices(builder.Configuration, logger, mediatRAssemblies);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
   // app.UseSwaggerUI();
}

app.UseAuthentication().UseAuthorization();

app.UseHttpsRedirection();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();
