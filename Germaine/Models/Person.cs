using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Models
{
    public class Person
    {
        public int PersonID {get; set;}
        
        public string FirstName {get; set;}

        public string MiddleName {get; set;}

        public string LastName {get; set;}

        public string Sex {get; set;}

        public string DateOfBirth {get; set;}

        public string PhoneNumber {get; set;}

        public string EmailAddress {get; set;}

        public string Address {get; set;}

        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set;}
    }
}