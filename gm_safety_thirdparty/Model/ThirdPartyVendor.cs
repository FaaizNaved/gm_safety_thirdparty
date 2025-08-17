namespace gm_safety_thirdparty.Models
{
    public class ThirdPartyVendor
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string ContactEmail { get; set; }
        public string SafetyCertificate { get; set; }
        public DateTime CertificateExpiry { get; set; }
        public bool IsCompliant { get; set; }
    }
}
