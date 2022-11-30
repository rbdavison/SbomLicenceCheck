using CommandLine;
using ConsoleTables;
using SbomLicenceCheck.Licences;
using SbomLicenceCheck.Manifests;
using SbomLicenceCheck.Output;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class ValidateLicenceActivity
    {
        [Verb("validate", false, HelpText = "Validate the licence types.")]
        public class Options
        {
            [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
            public string? bomFile { get; set; }

            [Option('i', "ids", Required = false, HelpText = "Specify valid licence reference numbers.")]
            public IEnumerable<int> validLicenceIds { get; set; } = Enumerable.Empty<int>();

            [Option('n', "names", Required = false, HelpText = "Specify valid licence names.")]
            public IEnumerable<string> validLicenceNames { get; set; } = Enumerable.Empty<string>();

            [Option('f', "format", Required = false, Default = OutputFormat.Markdown)]
            public OutputFormat format { get; set; }
        }

        public static async Task<int> Run(Options opts)
        {
            if (string.IsNullOrEmpty(opts.bomFile))
            {
                throw new ArgumentException("bomfile not specified");
            }

            if (!opts.validLicenceIds.Any() && !opts.validLicenceNames.Any())
            {
                throw new ArgumentException("valid licences not specified");
            }

            var output = OutputFactory.FormattedOutput(opts.format);
            var licencesFound = (await SoftwareManifest.ReadFile(opts.bomFile)).ComponentLicences;

            var invalidLicences = new Dictionary<string, List<Licence>>();
            
            foreach (var component in licencesFound.Keys)
            {
                foreach (var Licence in licencesFound[component])
                {
                    if (opts.validLicenceIds.Contains(Licence.ReferenceNumber) == false &&  
                        opts.validLicenceNames.Contains(Licence.LicenceId) == false)
                    {
                        if (invalidLicences.ContainsKey(component) == false)
                        {
                            invalidLicences[component] = new List<Licence>();
                        }

                        invalidLicences[component].Add(Licence);
                    }
                }
            }

            if (invalidLicences.Any())
            {
                output.RenderLicences(invalidLicences);
            }

            return invalidLicences.Any() ? 1 : 0;
        }

        public static void HandleError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Incorrect arguments, use --help");
        }
    }
}