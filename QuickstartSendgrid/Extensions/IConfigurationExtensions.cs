namespace Microsoft.Extensions.Configuration;

public static class IConfigurationExtensions
{
    public static void BindOptions<T>(this IConfiguration configuration, T options) where T : OptionsBase
    {
        var key = options.GetName();
        configuration
            .GetSection(key)
            .Bind(options);
    }
}
