using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Domain;

namespace WebApp.Helpers;

public class CustomLangStrBinder : IModelBinder
{
    private readonly ILogger<CustomLangStrBinder>? _logger;

    public CustomLangStrBinder(ILoggerFactory? loggerFactory)
    {
        _logger = loggerFactory?.CreateLogger<CustomLangStrBinder>();
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (value == null)
        {
            return Task.CompletedTask;
        }

        _logger?.LogDebug("LangStrBinder: {}", value);

        bindingContext.Result = ModelBindingResult.Success(new LangStr(value));

        return Task.CompletedTask;
    }
}
