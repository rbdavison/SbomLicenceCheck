using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Common
{
    public interface ILicenseRegistry
    {
        public string? LicenseListVersion { get; }

        public IEnumerable<License> Licenses { get; }
    }
}
