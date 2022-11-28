using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class ListLicenceActivity
    {
        [Verb("list", false, HelpText = "List and filter known licence types.")]
        public class Options
        {
            [Option('o', "osiApproved", Required = false, HelpText = "Only display OSI approved Licences.")]
            public bool osiApprovedOnly { get; set; }
        }

        public static int Run(Options opts)
        {
            var l = LicenceRegistry.Load();

            Console.WriteLine($"List version {l.LicenceListVersion}");

            var licences = l.Licences;

            if (opts.osiApprovedOnly)
            {
                licences = licences.Where(l => l.isOsiApproved);
            }

            var table = new ConsoleTable("id", "Licence Id", "Osi Approved?");

            foreach (var Licence in licences)
            {
                table.AddRow(Licence.ReferenceNumber, Licence.LicenceId, Licence.isOsiApproved);
            }

            table.Write(Format.MarkDown);

            return 0;
        }
    }
}