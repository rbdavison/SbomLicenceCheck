using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Manifests;

public class CheckLicenceActivity
{
    [Verb("check", true, HelpText = "Check the licence type.")]
    public class Options
    {
        [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
        public string? bomFile { get; set; }

        [Option('o', "osiApproved", Required = false, HelpText = "Only allow OSI approved licenses.")]
        public bool osiApprovedOnly { get; set; }
    }

    public static async Task<int> Run(Options opts)
    {
        if (string.IsNullOrEmpty(opts.bomFile))
        {
            HandleError(new List<Error>());
            return -1;
        }

        var licensesFound = (await SoftwareManifest.ReadFile(opts.bomFile)).ComponentLicences;

        var table = new ConsoleTable("Component", "Id", "Licence Id", "Osi Approved?");
        foreach (var component in licensesFound.Keys)
        {
            foreach (var license in licensesFound[component])
            {
                table.AddRow(component, license.ReferenceNumber, license.LicenseId, license.isOsiApproved);
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
