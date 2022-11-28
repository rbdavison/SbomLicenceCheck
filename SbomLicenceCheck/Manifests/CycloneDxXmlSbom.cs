using CycloneDX.Xml;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;
using System.Globalization;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxXmlSbom : ISoftwareManifest
    {
        private readonly ILicenceRegistry registry;
        private readonly Stream file;

        public IDictionary<string, List<Licence>> ComponentLicences
        {
            get; private set;
        } = new Dictionary<string, List<Licence>>();

        public CycloneDxXmlSbom(ILicenceRegistry LicenceRegistry, Stream file)
        {
            this.registry = LicenceRegistry ?? throw new ArgumentNullException(nameof(LicenceRegistry));
            this.file = file ?? throw new ArgumentNullException(nameof(file));
        }

        public Task Load()
        {
            var LicencesFound = new Dictionary<string, List<Licence>>();

            var bom = Serializer.Deserialize(this.file);

            foreach (var component in bom.Components)
            {
                if (LicencesFound.ContainsKey(component.Name) == false)
                {
                    LicencesFound[component.Name] = new List<Licence>();
                }

                foreach (var licence in component.Licenses)
                {
                    var Licence = this.registry.Licences.SingleOrDefault(
                        l => string.Compare(l.LicenceId, licence.License.Id, true, CultureInfo.InvariantCulture) == 0);
                    LicencesFound[component.Name].Add(Licence ?? Licence.UnknownLicence);
                }
            }

            this.ComponentLicences = LicencesFound;

            return Task.CompletedTask;
        }
    }
}
