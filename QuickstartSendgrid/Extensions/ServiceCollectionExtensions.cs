using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSendGridEmailService(this IServiceCollection services, IConfiguration configuration)
        => services
        .Configure<SmtpOptions>(options => configuration.BindOptions(options))
        .AddScoped<ISendGridClient, SendGridClient>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<SmtpOptions>>().Value;
            return new SendGridClient(options.ApiKey);
        }).AddScoped<SendEmailsService>();
}
