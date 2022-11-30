using ConsoleTables;
using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Output
{
    public class MarkdownOutput : IOutput
    {
        public void RenderLicences(IDictionary<string, List<Licence>> licences)
        {

            var table = new ConsoleTable("Component", "Id", "Licence Id", "Osi Approved?");

            foreach (var component in licences.Keys)
            {
                foreach (var Licence in licences[component])
                {
                    table.AddRow(component, Licence.ReferenceNumber, Licence.LicenceId, Licence.isOsiApproved);
                }
            }

            table.Write(Format.MarkDown);
        }

        public void RenderLicences(IEnumerable<Licence> licences)
        {
            var table = new ConsoleTable("id", "Licence Id", "Osi Approved?");

            foreach (var Licence in licences)
            {
                table.AddRow(Licence.ReferenceNumber, Licence.LicenceId, Licence.isOsiApproved);
            }

            table.Write(Format.MarkDown);
        }
    }
}
