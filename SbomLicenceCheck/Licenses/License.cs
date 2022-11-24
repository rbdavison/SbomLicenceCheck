namespace SbomLicenceCheck.Licenses
{
    public class License
    {
        public int ReferenceNumber { get; set; }
        public string? LicenseId { get; set; }
        public bool isOsiApproved { get; set; }
    }
}
