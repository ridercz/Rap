using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Areas.Admin.Models {
    public class UserCreateViewModel {

        [Display(Name = "Uživatelské jméno"), Required(ErrorMessage = "Není zadáno uživatelské jméno")]
        public string UserName { get; set; }

        [Display(Name = "E-mailová adresa"), DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Není zadán e-mail")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Zadaný e-mail je chybný")]
        public string EmailAddress { get; set; }

        [Display(Name = "Heslo"), Required(ErrorMessage = "Není zadáno heslo")]
        public string Password { get; set; }

    }
}