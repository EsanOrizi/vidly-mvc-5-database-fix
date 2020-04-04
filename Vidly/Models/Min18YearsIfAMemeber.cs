using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    // this is the class for a business rule that is any customer that is going on a membership type other than pas as you go has to be over 18
    // : ValidationAttribute to drive this class from ValidationAttribute
    public class Min18YearsIfAMemeber : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // 'validationContext.ObjectInstance' this gives us access to the containing class, in this case customer
            // because this is an object we need to cast it to the customer 
            var customer = (Customer)validationContext.ObjectInstance;

            // check the selected membership type 
            // if customer.MembershipTypeId == 1 i.e. is pay as you go
            // Or customer.MembershipTypeId == 0 i.e. user has not selected membership type yet, don't show error message

            // validation is successful 

            // refactoring magic numbers, replace 0 and 1 with corresponding value from membership type class  
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            // otherwise if birthdate is empty 
            if (customer.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            // calculate the age
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            // if age is greater than 18, validation is a success
            // otherwise the error message
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
        }
    }
}