using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Manifests;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class ValidateLicenceActivity
    {
        [Verb("validate", false, HelpText = "Validate the licence types.")]
        public class Options
        {
            [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
            public string? bomFile { get; set; }

            [Option('i', "ids", Required = true, HelpText = "Specify valid licence reference numbers.")]
            public IEnumerable<int>? validLicences { get; set; }
        }

        public static async Task<int> Run(Options opts)
        {
            if (string.IsNullOrEmpty(opts.bomFile))
            {
                throw new ArgumentException("bomfile not specified");
            }

            if (opts.validLicences == null || !opts.validLicences.Any())
            {
                throw new ArgumentException("valid licences not specified");
            }

            var LicencesFound = (await SoftwareManifest.ReadFile(opts.bomFile)).ComponentLicences;

            var invalidLicenceFound = false;

            var table = new ConsoleTable("Component", "Id", "Licence Id", "Osi Approved?");
            foreach (var component in LicencesFound.Keys)
            {
                foreach (var Licence in LicencesFound[component])
                {
                    if (opts.validLicences.Contains(Licence.ReferenceNumber) == false)
                    {
                        table.AddRow(component, Licence.ReferenceNumber, Licence.LicenceId, Licence.isOsiApproved);
                        invalidLicenceFound = true;
                    }
                }
            }

            if (invalidLicenceFound)
            {
                Console.WriteLine("Warning: Invalid Licences Found.");
                table.Write(Format.MarkDown);
            }
            else
            {
                Console.WriteLine("Success: No Invalid Licences Found.");
            }

            return invalidLicenceFound ? 1 : 0;
        }

        public static void HandleError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Incorrect arguments, use --help");
        }
    }
}