using System.Collections.Generic;

namespace Console.Module.Localization.Configurations
{
    public class LocalizationConfig
    {
        public string DefaultCulture { get; set; }
        public List<string> SupportedCultures { get; set; } = new();
    }
}
