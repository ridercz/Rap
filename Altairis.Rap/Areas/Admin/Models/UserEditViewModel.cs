using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Areas.Admin.Models {
    public class UserEditViewModel {
        [Display(Name = "E-mailová adresa"), DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Není zadán e-mail")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Zadaný e-mail je chybný")]
        public string EmailAddress { get; set; }

        [Display(Name = "Nové heslo")]
        public string NewPassword { get; set; }

        [Display(Name = "Uživatel je aktivní (smí se přihlásit)")]
        public bool IsApproved { get; set; }

    }
}