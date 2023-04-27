﻿namespace risk.control.system.Models
{
    public class ClientCompany
    {
        public string ClientCompanyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Addressline { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public State? State { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public string? PinCodeId { get; set; }
        public PinCode? PinCode { get; set; }

    }
    public class Address
    {
        public string AddressId { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string Addressline { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? StateId { get; set; }
        public State? State { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public string? PinCodeId { get; set; }
        public PinCode? PinCode { get; set; }
    }
}