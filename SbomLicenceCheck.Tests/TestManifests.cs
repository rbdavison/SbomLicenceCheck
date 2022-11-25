using SbomLicenceCheck.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public static Stream WheelCycloneDxJson
        {
            get
            {
                return ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), TestManifests.ManifestsDirectory, "wheel.cyclonedx.json");
            }
        }
    }
}
