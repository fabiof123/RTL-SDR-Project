using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace RtlSdrServer.Localization
{
    public static class RtlSdrServerLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(RtlSdrServerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(RtlSdrServerLocalizationConfigurer).GetAssembly(),
                        "RtlSdrServer.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
