using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        // override validation error message 
        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }



        [Display(Name = "Date of Birth")]
        // apply the attribute to birthdate 
        [Min18YearsIfAMemeber]
        public DateTime? Birthdate { get; set; }
    }
}