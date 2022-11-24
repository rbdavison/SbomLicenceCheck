using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Common
{
    public interface ISoftwareManifest
    {
        Task<IDictionary<string, List<License>>> GetComponentLicences();
    }
}
