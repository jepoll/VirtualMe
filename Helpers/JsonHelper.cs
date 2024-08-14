using System.Text.Json;

namespace Helpers;

public static class JsonHelper
{
    public static JsonSerializerOptions CamelCase = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
}
