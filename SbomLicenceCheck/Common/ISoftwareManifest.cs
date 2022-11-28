using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Common
{
    public interface ISoftwareManifest
    {
        Task Load();

        IDictionary<string, List<Licence>> ComponentLicences
        {
            get;
        }
    }
}
