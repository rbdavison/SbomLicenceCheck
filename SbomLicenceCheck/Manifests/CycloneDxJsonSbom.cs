using CycloneDX.Json;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;
using System.Globalization;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxJsonSbom : ISoftwareManifest
    {
        private readonly ILicenceRegistry LicenceRegistry;
        private readonly Stream fileStream;

        public CycloneDxJsonSbom(ILicenceRegistry LicenceRegistry, Stream fileStream)
        {
            this.LicenceRegistry = LicenceRegistry ?? throw new ArgumentNullException(nameof(LicenceRegistry));
            this.fileStream = fileStream ?? throw new ArgumentNullException(nameof(fileStream)); 
        }

        public IDictionary<string, List<Licence>> ComponentLicences 
        {
            get; private set;
        } = new Dictionary<string, List<Licence>>();

        public async Task Load()
        {
            var LicencesFound = new Dictionary<string, List<Licence>>();

            var bom = await Serializer.DeserializeAsync(this.fileStream);

            foreach (var component in bom.Components)
            {
                if (LicencesFound.ContainsKey(component.Name) == false)
                {
                    LicencesFound[component.Name] = new List<Licence>();
                }

                foreach (var licence in component.Licenses)
                {
                    var Licence = this.LicenceRegistry.Licences.SingleOrDefault(
                        l => string.Compare(l.LicenceId, licence.License.Id, true, CultureInfo.InvariantCulture) == 0);
                    LicencesFound[component.Name].Add(Licence ?? Licence.UnknownLicence);
                }
            }

            this.ComponentLicences = LicencesFound;
        }
    }
}
