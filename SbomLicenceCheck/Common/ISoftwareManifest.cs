using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Common
{
    public interface ISoftwareManifest
    {
        Task Load();

        IDictionary<string, List<License>> ComponentLicences
        {
            get;
        }
    }
}
