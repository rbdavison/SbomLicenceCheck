using CycloneDX.Xml;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;
using System.Globalization;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxXmlSbom : ISoftwareManifest
    {
        private readonly ILicenseRegistry registry;
        private readonly Stream file;

        public IDictionary<string, List<License>> ComponentLicences
        {
            get; private set;
        } = new Dictionary<string, List<License>>();

        public CycloneDxXmlSbom(ILicenseRegistry licenseRegistry, Stream file)
        {
            this.registry = licenseRegistry ?? throw new ArgumentNullException(nameof(licenseRegistry));
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public Task Load()
        {
            var licensesFound = new Dictionary<string, List<License>>();

            var bom = Serializer.Deserialize(this.file);

            foreach (var component in bom.Components)
            {
                if (licensesFound.ContainsKey(component.Name) == false)
                {
                    licensesFound[component.Name] = new List<License>();
                }

                foreach (var licence in component.Licenses)
                {
                    var license = this.registry.Licenses.SingleOrDefault(
                        l => string.Compare(l.LicenseId, licence.License.Id, true, CultureInfo.InvariantCulture) == 0);
                    licensesFound[component.Name].Add(license ?? License.UnknownLicense);
                }
            }

            this.ComponentLicences = licensesFound;

            return Task.CompletedTask;
        }
    }
}
