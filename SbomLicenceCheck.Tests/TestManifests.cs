using SbomLicenceCheck.Utils;
using System.Reflection;

namespace SbomLicenceCheck.Tests
{
    public static class TestManifests
    {
        private const string ManifestsDirectory = "TestManifests";


        public static Stream AlpineCycloneDxXml
        {
            get
            {
                return ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), TestManifests.ManifestsDirectory, "alpine.cyclonedx.xml");
            }
        }

        public static Stream PillowCycloneDxJson
        {
            get
            {
                return ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), TestManifests.ManifestsDirectory, "pillow.cyclonedx.json");
            }
        }

        public static Stream CycloneDxSchema16Xml
        {
            get
            {
                return ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), TestManifests.ManifestsDirectory, "sbomlicensecheck.cyclonedx.xml");
            }
        }

        public static Stream WheelCycloneDxJson
        {
            get
            {
                return ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), TestManifests.ManifestsDirectory, "wheel.cyclonedx.json");
            }
        }
    }
}
