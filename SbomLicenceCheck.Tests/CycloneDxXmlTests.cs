using CycloneDX.Models;
using Moq;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;
using SbomLicenceCheck.Manifests;

namespace SbomLicenceCheck.Tests
{
    public class CycloneDxXmlTests
    {
        [Test]
        public async Task BasicSbomReading()
        {
            var registry = LicenseRegistry.Load();

            var sbomFormat = new CycloneDxXmlSbom(registry, TestManifests.AlpineCycloneDxXml);
            await sbomFormat.Load();

            Assert.IsTrue(sbomFormat.ComponentLicences.Any());
            Assert.IsTrue(sbomFormat.ComponentLicences["alpine-baselayout"].Any());
        }
    }
}