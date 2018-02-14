using System;
using System.ComponentModel.DataAnnotations;
using Germaine.Models;

namespace Germaine.Dtos
{
    public class PersonForListDtos
    {
        public string FirstName {get; set;}

        public string MiddleName {get; set;}

        public string LastName {get; set;}

        public string Sex {get; set;}

        public string PhoneNumber {get; set;}

        public string EmailAddress {get; set;}

        

    }
}