using Microsoft.Extensions.Localization;
using System.Reflection;
using Newtonsoft.Json;

namespace WebProje.Services
{
    public class SharedResource
    {
    }
    public class LanguageService
    {

        private readonly IStringLocalizer _Localizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);

            _Localizer = factory.Create(nameof(SharedResource), assemblyName.Name);
        }

        public String GetKey(string key)
        {
            return _Localizer[key].Value;
        }
        
        public Dictionary<string, string>LocalizerToJson()
        {
            var localizedData = new Dictionary<string, string>();

            foreach (var resource in _Localizer.GetAllStrings(true))
            {
                localizedData[resource.Name] = resource.Value;
            }

            return localizedData;
        }
        
    }
}