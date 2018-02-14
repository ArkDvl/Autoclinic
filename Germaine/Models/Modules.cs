using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Models
{
    public class Modules
    {
        public int ModulesID {get; set;}
        
        public string Name {get; set;}

        public string Key {get; set;}

        public DateTime CreatedAt {get; set;}
        
    }
}