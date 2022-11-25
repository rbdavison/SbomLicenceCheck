using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Manifests
{
    public class SoftwareManifest
    {
        public static async Task<ISoftwareManifest> ReadFile(string filename)
        {
            ISoftwareManifest? sbomManifest = null;
            if (File.Exists(filename) == false)
            {
                throw new FileNotFoundException(filename);
            }

            var licenseRegistry = LicenseRegistry.Load();
            
            using (var fs = File.OpenRead(filename))
            {
                if (filename.EndsWith(".xml"))
                {
                    sbomManifest = new CycloneDxXmlSbom(licenseRegistry, fs);
                }
                else if (filename.EndsWith(".json"))
                {
                    sbomManifest = new CycloneDxJsonSbom(licenseRegistry, fs);
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
