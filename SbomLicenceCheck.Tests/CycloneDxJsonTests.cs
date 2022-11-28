using CycloneDX.Models;
using Moq;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;
using SbomLicenceCheck.Manifests;

namespace SbomLicenceCheck.Tests
{
    public class CycloneDxJsonTests
    {
        [Test]
        public async Task BasicSbomReading()
        {
            var registry = LicenceRegistry.Load();

            var sbomFormat = new CycloneDxJsonSbom(registry, TestManifests.PillowCycloneDxJson);
            await sbomFormat.Load();

            Assert.IsTrue(sbomFormat.ComponentLicences.Any());
            Assert.IsTrue(sbomFormat.ComponentLicences["Pillow"].Any());
        }

        [Test]
        public async Task BasicSbomReading2()
        {
            var registry = LicenceRegistry.Load();

            var sbomFormat = new CycloneDxJsonSbom(registry, TestManifests.WheelCycloneDxJson);
            await sbomFormat.Load();

            Assert.IsTrue(sbomFormat.ComponentLicences.Any());
            Assert.IsTrue(sbomFormat.ComponentLicences["wheel"].Any());
        }
    }
}