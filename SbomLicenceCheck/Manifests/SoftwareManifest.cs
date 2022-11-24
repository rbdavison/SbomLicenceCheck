using SbomLicenceCheck.Common;

namespace SbomLicenceCheck.Manifests
{
    public class SoftwareManifest
    {
        public static ISoftwareManifest ReadFile(string filename)
        {
            if (filename.EndsWith(".xml"))
            {
                return new CycloneDxXmlSbom(filename);
            }

            if (filename.EndsWith(".json"))
            {
                return new CycloneDxJsonSbom(filename);
            }

            throw new InvalidOperationException("Unrecognised file extension.");
        }
    }
}
