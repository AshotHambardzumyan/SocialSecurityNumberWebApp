using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialSecurityNumberWebApp.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public DateTime ReportDateTime { get; set; }

        [Required]
        public string Country { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime BirthDateTime { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string SureName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string SSN { get; set; }

        [Required]
        public bool Male { get; set; }

        [Required]
        public bool Female { get; set; }
    }
}
