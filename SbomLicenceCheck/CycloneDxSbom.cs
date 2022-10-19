using System.Xml;

namespace SbomLicenceCheck
{
    internal class CycloneDxSbom
    {
        public IDictionary<string, List<License>> GetComponentLicences(string sbom)
        {
            var licensesFound = new Dictionary<string, List<License>>();

            var l = LicenseRegistry.Load();
            XmlDocument doc = new XmlDocument();
            doc.Load(sbom);

            XmlNode root = doc.DocumentElement;

            // Add the namespace.  
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bom", "http://cyclonedx.org/schema/bom/1.4");

            // Get library components only.
            XmlNodeList componentList = root.SelectNodes("/bom:bom/bom:components/bom:component[@type='library']", nsmgr);

            if (componentList != null && componentList.Count > 0)
            {
                foreach (XmlNode component in componentList)
                {
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
                            var license = l.Licenses.SingleOrDefault(l => string.Compare(l.LicenseId, lincenseId, true) == 0);
                            licensesFound[componentName].Add(license ?? new License { LicenseId = "Unknown", ReferenceNumber = -1 });
                        }
                    }
                }
            }

            return licensesFound;
        }
    }
}
