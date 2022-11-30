using SbomLicenceCheck.Licences;

namespace SbomLicenceCheck.Common
{
    public interface IOutput
    {
        void RenderLicences(IDictionary<string, List<Licence>> licences);
        void RenderLicences(IEnumerable<Licence> licences);
    }
}
