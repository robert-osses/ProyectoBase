using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sicade.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        public string RUT { get; set; }
        [Required]
        public string  PASSWORD { get; set; }
    }
}
