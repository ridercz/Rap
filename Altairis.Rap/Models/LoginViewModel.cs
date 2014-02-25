using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Models {
    public class LoginViewModel {
        [Display(Name = "Uživatelské jméno")]
        [Required(ErrorMessage = "Není zadáno uživatelské jméno")]
        public string UserName { get; set; }

        [Display(Name = "Heslo"), DataType(DataType.Password)]
        [Required(ErrorMessage = "Není zadáno heslo")]
        public string Password { get; set; }

        [Display(Name = "Pamatuj si mne na tomto počítači")]
        public bool RememberMe { get; set; }
    }
}