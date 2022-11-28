using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Manifests;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class CheckLicenceActivity
    {
        [Verb("check", true, HelpText = "Check the licence type.")]
        public class Options
        {
            [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
            public string? bomFile { get; set; }

            [Option('o', "osiApproved", Required = false, HelpText = "Only allow OSI approved Licences.")]
            public bool osiApprovedOnly { get; set; }
        }

        public static async Task<int> Run(Options opts)
        {
            if (string.IsNullOrEmpty(opts.bomFile))
            {
                HandleError(new List<Error>());
                return -1;
            }

            var LicencesFound = (await SoftwareManifest.ReadFile(opts.bomFile)).ComponentLicences;

            var table = new ConsoleTable("Component", "Id", "Licence Id", "Osi Approved?");
            foreach (var component in LicencesFound.Keys)
            {
                foreach (var Licence in LicencesFound[component])
                {
                    table.AddRow(component, Licence.ReferenceNumber, Licence.LicenceId, Licence.isOsiApproved);
                }
            }

            table.Write(Format.MarkDown);

            return 0;
        }

        private static void HandleError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Incorrect arguments, use --help");
        }
    }
}