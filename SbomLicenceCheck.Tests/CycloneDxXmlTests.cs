using SbomLicenceCheck.Licences;
using SbomLicenceCheck.Manifests;

namespace SbomLicenceCheck.Tests
{
    public class CycloneDxXmlTests
    {
        [Test]
        public async Task BasicSbomReading()
        {
            var registry = LicenceRegistry.Load();

            var sbomFormat = new CycloneDxXmlSbom(registry, TestManifests.AlpineCycloneDxXml);
            await sbomFormat.Load();

            Assert.IsTrue(sbomFormat.ComponentLicences.Any());
            Assert.IsTrue(sbomFormat.ComponentLicences["alpine-baselayout"].Any());
        }

        [Test]
        public async Task CycloneDXSchema16()
        {
            var registry = LicenceRegistry.Load();

            var sbomFormat = new CycloneDxXmlSbom(registry, TestManifests.CycloneDxSchema16Xml);
            await sbomFormat.Load();

            Assert.IsTrue(sbomFormat.ComponentLicences.Any());
            Assert.IsTrue(sbomFormat.ComponentLicences["CycloneDX.Core"].Any());
        }
    }
}