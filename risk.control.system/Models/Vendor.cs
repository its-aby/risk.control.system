using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace risk.control.system.Models
{
    public class Vendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VendorId { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Addressline { get; set; } = string.Empty;
        public List<VendorInvestigationServiceType> VendorInvestigationServiceTypes { get; set; }
        public string City { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public State? State { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public string? PinCodeId { get; set; }
        public PinCode? PinCode { get; set; }
    }

    public class VendorInvestigationServiceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string VendorInvestigationServiceTypeId { get; set; } = Guid.NewGuid().ToString();
        public InvestigationServiceType InvestigationServiceType { get; set; }
        public List<ServicedPinCode> PincodeServices { get; set; }
    }

    public class ServicedPinCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicedPinCodeId { get; set; }
        public string Pincode { get; set; }
    }
}
