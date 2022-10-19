using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using SbomLicenceCheck.Utils;

namespace SbomLicenceCheck
{
    internal class License
    {
        public int ReferenceNumber { get; set; }
        public string? LicenseId { get; set; }

        public bool isOsiApproved { get; set; }
    }

    internal class LicenseRegistry
    {
        public string? LicenseListVersion { get; set; } = "Unknown";
        public IEnumerable<License> Licenses { get; set; } = Enumerable.Empty<License>();

        public static LicenseRegistry Load()
        {
            var stream = ResourceHelper.ReadResource(Assembly.GetExecutingAssembly(), "Licenses", "spdxlicences.json");

            return JsonSerializer.Deserialize<LicenseRegistry>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
