using CycloneDX.Json;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxJsonSbom : ISoftwareManifest
    {
        private readonly ILicenseRegistry licenseRegistry;
        private readonly Stream file;

        public CycloneDxJsonSbom(ILicenseRegistry licenseRegistry, Stream file)
        {
            this.licenseRegistry = licenseRegistry ?? throw new ArgumentNullException(nameof(licenseRegistry));
            this.file = file ?? throw new ArgumentNullException(nameof(file)); 
        }

        public IDictionary<string, List<License>> ComponentLicences 
        {
            get; private set;
        }

        public async Task Load()
        {
            var licensesFound = new Dictionary<string, List<License>>();

            var bom = await Serializer.DeserializeAsync(this.file);

            foreach (var component in bom.Components)
            {
                if (licensesFound.ContainsKey(component.Name) == false)
                {
                    licensesFound[component.Name] = new List<License>();
                }

                foreach (var licence in component.Licenses)
                {
                    var license = this.licenseRegistry.Licenses.SingleOrDefault(l => string.Compare(l.LicenseId, licence.License.Id, true) == 0);
                    licensesFound[component.Name].Add(license ?? License.UnknownLicense);
                }
            }

            this.ComponentLicences = licensesFound;
        }
    }
}
