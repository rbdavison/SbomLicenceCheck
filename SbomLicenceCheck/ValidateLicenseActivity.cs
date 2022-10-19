using CommandLine;
using SbomLicenceCheck;
using ConsoleTables;

public class ValidateLicenseActivity
{
    [Verb("validate", false, HelpText = "Validate the licence types.")]
    public class Options
    {
        [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
        public string? bomFile { get; set; }

        [Option('i', "ids", Required = true, HelpText = "Specify valid licence reference numbers.")]
        public IEnumerable<int>? validLicenses { get; set; }
    }

    public static int Run(Options opts)
    {
        var licensesFound = new CycloneDxSbom().GetComponentLicences(opts.bomFile);

        var invalidLicenseFound = false;

        var table = new ConsoleTable("Component", "Id", "Licence Id", "Osi Approved?");
        foreach (var component in licensesFound.Keys)
        {
            foreach (var license in licensesFound[component])
            {
                if (opts.validLicenses.Contains(license.ReferenceNumber) == false)
                {
                    table.AddRow(component, license.ReferenceNumber, license.LicenseId, license.isOsiApproved);
                    invalidLicenseFound = true;
                }
            }
        }

        if (invalidLicenseFound)
        {
            Console.WriteLine("Invalid Licenses Found.");
            table.Write(Format.MarkDown);
        }

        return invalidLicenseFound ? 1 : 0;
    }

    public static void HandleError(IEnumerable<Error> errors)
    {
        Console.WriteLine("Incorrect arguments, use --help");
    }
}





