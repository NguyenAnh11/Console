using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using System;

namespace Console.Module.Localization
{
    public class JsonStringlocalizerFactory : IStringLocalizerFactory
    {
        private readonly IMemoryCache _memoryCache;

        public JsonStringlocalizerFactory(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IStringLocalizer Create(Type resourceSource)
            => new JsonStringLocalizer(_memoryCache);

        public IStringLocalizer Create(string baseName, string location)
            => new JsonStringLocalizer(_memoryCache);
    }
}
