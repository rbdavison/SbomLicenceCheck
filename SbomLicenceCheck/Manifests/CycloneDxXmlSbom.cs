using System.Xml;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.Manifests
{
    public class CycloneDxXmlSbom : ISoftwareManifest
    {
        private readonly string filename;

        public CycloneDxXmlSbom(string filename)
        {
            this.filename = filename;
        }

        // Replace this with: https://github.com/CycloneDX/cyclonedx-dotnet-library

        public Task<IDictionary<string, List<License>>> GetComponentLicences()
        {
            var licensesFound = new Dictionary<string, List<License>>();

            if (File.Exists(filename) == false)
            {
                throw new InvalidOperationException($"File '{filename}' not found.");
            }

            var licenseRegistry = LicenseRegistry.Load();
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            if (doc.DocumentElement == null)
            {
                throw new InvalidOperationException("Document doesn't contain root node.");
            }

            XmlNode root = doc.DocumentElement;

            // Add the namespace.  
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bom", "http://cyclonedx.org/schema/bom/1.4");

            // Get library components only.
            XmlNodeList? componentList = root.SelectNodes("/bom:bom/bom:components/bom:component[@type='library']", nsmgr);

            if (componentList != null && componentList.Count > 0)
            {
                foreach (XmlNode component in componentList)
                {
                    if (component == null)
                    {
                        continue;
                    }

                    var componentName = component["name"]?.InnerText ?? "Unknown";
                    if (licensesFound.ContainsKey(componentName) == false)
                    {
                        licensesFound[componentName] = new List<License>();
                    }

                    XmlNodeList licenceList = component.SelectNodes("bom:licenses/bom:license", nsmgr);
                    if (licenceList != null && licenceList.Count > 0)
                    {
                        foreach (XmlNode licence in licenceList)
                        {
                            var lincenseId = licence["id"]?.InnerText ?? "Unknown";
                            var license = licenseRegistry.Licenses.SingleOrDefault(l => string.Compare(l.LicenseId, lincenseId, true) == 0);
                            licensesFound[componentName].Add(license ?? new License { LicenseId = "Unknown", ReferenceNumber = -1 });
                        }
                    }
                }
            }

            return Task.FromResult(licensesFound as IDictionary<string, List<License>>);
        }
    }
}
