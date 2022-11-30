using CommandLine;
using SbomLicenceCheck.Manifests;
using SbomLicenceCheck.Output;

namespace SbomLicenceCheck.UI.CommandLine
{
    public class CheckLicenceActivity
    {
        [Verb("check", false, HelpText = "Check the licence type.")]
        public class Options
        {
            [Option('b', "bom", Required = true, HelpText = "Set bom filename.")]
            public string? bomFile { get; set; }

            [Option('o', "osiApproved", Required = false, HelpText = "Only allow OSI approved Licences.")]
            public bool osiApprovedOnly { get; set; }

            [Option('f', "format", Required = false, Default = OutputFormat.Markdown)]
            public OutputFormat format { get; set; }
        }

        public static async Task<int> Run(Options opts)
        {
            if (string.IsNullOrEmpty(opts.bomFile))
            {
                HandleError(new List<Error>());
                return -1;
            }

            var output = OutputFactory.FormattedOutput(opts.format);

            var licencesFound = (await SoftwareManifestFactory.ReadFile(opts.bomFile)).ComponentLicences;
            output.RenderLicences(licencesFound);
            
            return 0;
        }

        private static void HandleError(IEnumerable<Error> errors)
        {
            Console.WriteLine("Incorrect arguments, use --help");
        }
    }
}