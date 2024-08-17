using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Reflection;

namespace RiverBooks.EmailSending
{
    public static class EmailSendingServiceExtension
    {
        
        public static IServiceCollection AddEmailService(this IServiceCollection services, ConfigurationManager configurationManager, ILogger logger, List<Assembly> mediatRAssemblies)
        {
            mediatRAssemblies.Add(typeof(EmailSendingServiceExtension).Assembly);

            logger.Information("{Module} module has been registered", "Email");

            services.AddScoped<ISendEmail, MimeKitEmailSender>();

            return services;
        }
    }
}
