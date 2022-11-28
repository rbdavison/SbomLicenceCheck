namespace SbomLicenceCheck.Licences
{
    public class Licence
    {
        public int ReferenceNumber { get; set; }
        public string? LicenceId { get; set; }
        public bool isOsiApproved { get; set; }

        public static Licence UnknownLicence
        {
            get
            {
                return new Licence 
                { 
                    LicenceId = "Unknown", 
                    ReferenceNumber = -1, 
                    isOsiApproved = true 
                };
            }
        }
    }
}
