using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Licenses;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class ListLicenseActivity
    {
        [Verb("list", false, HelpText = "List and filter known licence types.")]
        public class Options
        {
            [Option('o', "osiApproved", Required = false, HelpText = "Only display OSI approved licenses.")]
            public bool osiApprovedOnly { get; set; }
        }

        public static int Run(Options opts)
        {
            var l = LicenseRegistry.Load();

            Console.WriteLine($"List version {l.LicenseListVersion}");

            var licences = l.Licenses;

            if (opts.osiApprovedOnly)
            {
                licences = licences.Where(l => l.isOsiApproved);
            }

            var table = new ConsoleTable("id", "Licence Id", "Osi Approved?");

            foreach (var license in licences)
            {
                table.AddRow(license.ReferenceNumber, license.LicenseId, license.isOsiApproved);
            }

            table.Write(Format.MarkDown);

            return 0;
        }
    }
}