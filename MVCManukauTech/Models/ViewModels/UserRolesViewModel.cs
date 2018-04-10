using MVCManukauTech.Models.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCManukauTech.Models.ViewModels
{
    public class UserRolesViewModel
    {
        [Key]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        //public string Culture { get; set; }
        //public string Description { get; set; }
    }
}