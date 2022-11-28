using SbomLicenceCheck.Common;
using SbomLicenceCheck.Utils;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SbomLicenceCheck.Licences
{
    public class LicenceRegistry : ILicenceRegistry
    {
        [JsonPropertyName("licenseListVersion")]
        public string? LicenceListVersion { get; set; } = "Unknown";

        [JsonPropertyName("Licenses")]
        public IEnumerable<Licence> Licences { get; set; } = Enumerable.Empty<Licence>();

        public static LicenceRegistry Load()
        {
            var stream = ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), "Licences", "spdxlicences.json");

            var registry = JsonSerializer.Deserialize<LicenceRegistry>(
                stream, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (registry == null || !registry.Licences.Any())
            {
                throw new InvalidOperationException("Failed to deserialize licence types.");
            }

            return registry;
        }
    }
}
