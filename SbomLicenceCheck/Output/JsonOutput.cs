using SbomLicenceCheck.Common;
using SbomLicenceCheck.Licences;
using System.Text.Json;

namespace SbomLicenceCheck.Output
{
    public class JsonOutput : IOutput
    {
        private JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        public void RenderLicences(IDictionary<string, List<Licence>> licences)
        {
            var jsonString = JsonSerializer.Serialize(licences, this.options);
            Console.Write(jsonString);
        }

        public void RenderLicences(IEnumerable<Licence> licences)
        {
            var jsonString = JsonSerializer.Serialize(licences, this.options);
            Console.Write(jsonString);
        }
    }
}
