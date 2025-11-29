using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Xml.Linq;

namespace CustomInteriors.Web
{
    public class XmlConfigProvider : IConfigurationProvider
    {
        private readonly string _path;
        private readonly string _environment;
        private readonly Dictionary<string, string> _data = new(StringComparer.OrdinalIgnoreCase);

        public XmlConfigProvider(string path, string environment)
        {
            _path = path;
            _environment = environment;
            LoadXml();
        }

        private void LoadXml()
        {
            var xml = XDocument.Load(_path);
            var root = xml.Root;

            if (root == null) return;

            foreach (var cfg in root.Elements("config"))
            {
                var key = cfg.Attribute("key")?.Value;
                if (string.IsNullOrWhiteSpace(key)) continue;

                var settingsList = cfg.Element("settingsList")?.Elements("setting") ?? Enumerable.Empty<XElement>();
                foreach (var setting in settingsList)
                {
                    var namesAttr = setting.Attribute("name")?.Value;
                    if (string.IsNullOrWhiteSpace(namesAttr)) continue;

                    var names = namesAttr.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (names.Contains(_environment, StringComparer.OrdinalIgnoreCase))
                    {
                        _data[key] = setting.Attribute("value")?.Value ?? "";
                        break; // first matching environment wins
                    }
                }
            }
        }

        // Return simple key/value
        public bool TryGet(string key, out string value) => _data.TryGetValue(key, out value);

        public void Set(string key, string value) => _data[key] = value;

        public IChangeToken GetReloadToken() => NullChangeToken.Singleton;

        public void Load() { /* Already loaded in constructor */ }

        // Correct hierarchical child keys
        public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
        {
            if (string.IsNullOrEmpty(parentPath))
            {
                return _data.Keys
                    .Select(k => k.Split(':', 2)[0])
                    .Distinct()
                    .Concat(earlierKeys)
                    .OrderBy(k => k, StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                return _data.Keys
                    .Where(k => k.StartsWith(parentPath + ":", StringComparison.OrdinalIgnoreCase))
                    .Select(k => k.Substring(parentPath.Length + 1).Split(':', 2)[0])
                    .Distinct()
                    .Concat(earlierKeys)
                    .OrderBy(k => k, StringComparer.OrdinalIgnoreCase);
            }
        }
    }

    // Extension for easy use in Program.cs
    public static class XmlConfigExtensions
    {
        public static IConfigurationBuilder AddXmlConfig(this IConfigurationBuilder builder, string path, string environment)
        {
            return builder.Add(new XmlConfigSource(path, environment));
        }
    }

    public class XmlConfigSource : IConfigurationSource
    {
        private readonly string _path;
        private readonly string _environment;

        public XmlConfigSource(string path, string environment)
        {
            _path = path;
            _environment = environment;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
            => new XmlConfigProvider(_path, _environment);
    }
}
