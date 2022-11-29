using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Tests
{
    public class LicenseRegistryTests
    {
        [Test]
        public void BasicLoadTest()
        {
            var registry = LicenceRegistry.Load();
            Assert.That(registry.Licences.Count(), Is.EqualTo(497));

            foreach (var licences in registry.Licences)
            {
                Assert.That(licences.LicenceId, Is.Not.EqualTo("Unknown"));
                Assert.That(licences.ReferenceNumber, Is.Not.EqualTo(-1));
            }
        }
    }
}