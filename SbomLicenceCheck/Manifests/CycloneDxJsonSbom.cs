using CycloneDX.Json;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxJsonSbom : ISoftwareManifest
    {
        private readonly string filename;

        public CycloneDxJsonSbom(string filename)
        {
            this.filename = filename;
        }

        public async Task<IDictionary<string, List<License>>> GetComponentLicences()
        {
            var licensesFound = new Dictionary<string, List<License>>();

            if (File.Exists(filename) == false)
            {
                throw new InvalidOperationException($"File '{filename}' not found.");
            }

            var licenseRegistry = LicenseRegistry.Load();

            using (var fs = File.OpenRead(this.filename))
            {
                var bom = await Serializer.DeserializeAsync(fs);

                foreach (var component in bom.Components)
                {
                    if (licensesFound.ContainsKey(component.Name) == false)
                    {
                        licensesFound[component.Name] = new List<License>();
                    }

                    foreach (var licence in component.Licenses)
                    {
                        var license = licenseRegistry.Licenses.SingleOrDefault(l => string.Compare(l.LicenseId, licence.License.Id, true) == 0);
                        licensesFound[component.Name].Add(license ?? new License { LicenseId = "Unknown", ReferenceNumber = -1 });
                    }
                }
            }

            return licensesFound;
        }
    }
}
