using SbomLicenceCheck.Common;

namespace SbomLicenceCheck.Output
{
    public enum OutputFormat
    {
        Markdown = 0,
        Json = 1
    }

    public static class OutputFactory
    {
        public static IOutput FormattedOutput(OutputFormat format = OutputFormat.Markdown)
        {
            switch (format)
            {
                case OutputFormat.Markdown:
                    return new MarkdownOutput();
                case OutputFormat.Json:
                    return new JsonOutput();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
