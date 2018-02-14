using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Dtos
{
    public class UserForModuleAuthDtos
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public int ModuleID { get; set; }
    }
}