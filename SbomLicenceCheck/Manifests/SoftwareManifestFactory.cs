using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Manifests
{
    public class SoftwareManifestFactory
    {
        public static async Task<ISoftwareManifest> ReadFile(string filename)
        {
            ISoftwareManifest? sbomManifest = null;
            if (File.Exists(filename) == false)
            {
                throw new FileNotFoundException(filename);
            }

            var licenceRegistry = LicenceRegistry.Load();
            
            using (var fs = File.OpenRead(filename))
            {
                if (filename.EndsWith(".xml", StringComparison.InvariantCulture))
                {
                    sbomManifest = new CycloneDxXmlSbom(licenceRegistry, fs);
                }
                else if (filename.EndsWith(".json", StringComparison.InvariantCulture))
                {
                    sbomManifest = new CycloneDxJsonSbom(licenceRegistry, fs);
                }
                else 
                {
                    throw new InvalidOperationException("Unrecognised file extension.");
                }

                await sbomManifest.Load();
            }

            return sbomManifest;
        }
    }
}
