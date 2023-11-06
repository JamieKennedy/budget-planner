using Common.Results.Error.Base;

namespace Common.Results.Error.Configuration
{
    public class ConfigurationItemNotFoundError : NotFoundError
    {
        public ConfigurationItemNotFoundError(string key) : base($"Configuration item with key {key} not found")
        {
        }
    }
}
