using Common.Exceptions.Base;

namespace Common.Exceptions.Configuration
{
    public class ConfigurationItemNotFoundException : NotFoundException
    {
        public ConfigurationItemNotFoundException(string key) : base($"Configuration item with key {key} not found")
        {
        }
    }
}
