using System.Text.Json;

namespace MinimalEndpoints.Domain.Settings;

public static class SerializerSettings
{
    public static JsonSerializerOptions Default { get; } = BuildSerializerSettings();

    private static JsonSerializerOptions BuildSerializerSettings()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return options;
    }
}
