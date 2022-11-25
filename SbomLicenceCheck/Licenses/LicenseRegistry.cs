using SbomLicenceCheck.Common;
using SbomLicenceCheck.Utils;
using System.Reflection;
using System.Text.Json;

namespace SbomLicenceCheck.Licenses
{

    public class LicenseRegistry : ILicenseRegistry
    {
        public string? LicenseListVersion { get; set; } = "Unknown";
        public IEnumerable<License> Licenses { get; set; } = Enumerable.Empty<License>();

        public static LicenseRegistry Load()
        {
            var stream = ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), "Licenses", "spdxlicences.json");

            var registry = JsonSerializer.Deserialize<LicenseRegistry>(
                stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (registry == null)
            {
                throw new InvalidOperationException("Failed to deserialize licence types.");
            }

            return registry;
        }
    }
}
