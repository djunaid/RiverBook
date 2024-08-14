using RiverBooks.Books;
using FastEndpoints;
using RiverBooks.Users;
using Serilog;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using System.Reflection;
using RiverBooks.OrderProcessing;
using RiverBooks.SharedKernel;
using RiverBooks.Users.UseCases.Cart.AddItem;

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
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

List<Assembly> mediatRAssemblies = [typeof(Program).Assembly];


// add service dependency for modules
builder.Services.AddFastEndpoints()    
    .AddJWTBearerAuth(builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization()
    .SwaggerDocument();
    

builder.Services.AddBookModuleService(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddOrderProcessingModuleServices(builder.Configuration, logger, mediatRAssemblies);
builder.Services.AddUserModuleServices(builder.Configuration, logger, mediatRAssemblies);

builder.Services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

builder.Services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

builder.Services.AddLoggingBehavior();
builder.Services.AddFluentValidationBehavior();
builder.Services.AddValidatorsFromAssemblyContaining<AddCartItemCommandValidator>();

var app = builder.Build();


app.UseAuthentication().UseAuthorization();

//app.UseHttpsRedirection();
app.UseFastEndpoints().UseSwaggerGen();

app.Run();
