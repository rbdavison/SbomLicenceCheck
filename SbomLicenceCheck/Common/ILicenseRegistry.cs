using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Common
{
    public interface ILicenceRegistry
    {
        public string? LicenceListVersion { get; }

        public IEnumerable<Licence> Licences { get; }
    }
}
