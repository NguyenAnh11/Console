using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Console.Module.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IMemoryCache _memoryCache;
        private readonly object _lockObject = new();

        public JsonStringLocalizer(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value);
            }
        }

        protected string GetString(string name)
        {
            if (name == null || name.Trim().Length == 0)
                throw new ArgumentNullException(nameof(name));

            var resources = LoadResources();

            var resource = resources.FirstOrDefault(p => p.Name == name);

            if (resource == null)
                return null;

            return resource.Value;
        }

        protected IEnumerable<LocalizedString> LoadResources(string culture = null)
        {
            culture ??= Thread.CurrentThread.CurrentCulture.Name;

            if (_memoryCache.TryGetValue(culture, out List<LocalizedString> resources))
                return resources;

            lock (_lockObject)
            {
                if (_memoryCache.TryGetValue(culture, out resources))
                    return resources;

                var relativePath = $"Resources/{culture}.json";

                var absolutePath = Path.GetFullPath(relativePath);

                if (!File.Exists(absolutePath))
                    throw new FileNotFoundException("File doesn't exist", absolutePath);

                using var fs = new FileStream(absolutePath, FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);
                using var reader = new JsonTextReader(sr);

                resources = new List<LocalizedString>();

                while (reader.Read())
                {
                    if (reader.TokenType != JsonToken.PropertyName)
                        continue;

                    var key = (string)reader.Value;
                    reader.Read();
                    var value = (string)reader.Value;
                    resources.Add(new LocalizedString(key, value));
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _memoryCache.Set(culture, resources, cacheEntryOptions);
            }

            return resources;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => LoadResources();
    }
}
