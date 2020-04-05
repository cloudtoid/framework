namespace Cloudtoid
{
    using System;
    using System.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using static Contract;

    [DebuggerStepThrough]
    public static class ConfigurationExtensions
    {
        public static T GetValueSafe<T>(this IConfiguration configuration, string key, T defaultValue)
        {
            CheckValue(configuration, nameof(configuration));

            var section = configuration.GetSection(key);
            if (section is null || string.IsNullOrEmpty(section.Key))
                return defaultValue;

            try
            {
                return configuration.GetValue(key, defaultValue) ?? defaultValue;
            }
            catch (Exception ex) when (!ex.IsFatal())
            {
                return defaultValue;
            }
        }

        public static string GetValueSafe(this IConfiguration configuration, string key, string defaultValue)
        {
            CheckValue(configuration, nameof(configuration));

            var section = configuration.GetSection(key);
            if (section is null || string.IsNullOrEmpty(section.Key))
                return defaultValue;

            return section.Value;
        }
    }
}
