namespace SbomLicenceCheck.Licenses
{
    public class License
    {
        public int ReferenceNumber { get; set; }
        public string? LicenseId { get; set; }
        public bool isOsiApproved { get; set; }

        public static License UnknownLicense
        {
            get
            {
                return new License 
                { 
                    LicenseId = "Unknown", 
                    ReferenceNumber = -1, 
                    isOsiApproved = true 
                };
            }
        }
    }
}
