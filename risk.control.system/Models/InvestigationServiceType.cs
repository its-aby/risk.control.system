﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace risk.control.system.Models
{
    public class InvestigationServiceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string InvestigationServiceTypeId { get; set; } = Guid.NewGuid().ToString();
        [Display(Name = "InvestigationService Type")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "InvestigationService Type code")]
        [Required]
        public string Code { get; set; }
        public string LineOfBusinessId { get; set; }
        public LineOfBusiness LineOfBusiness { get; set; }

    }
}
