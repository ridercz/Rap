using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Models {
    public class AccountViewModel {

        [Display(Name = "E-mailová adresa"), DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Není zadán e-mail")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Zadaný e-mail je chybný")]
        public string EmailAddress { get; set; }

        [Display(Name = "Poslat e-mail, když někdo udělá novou rezervaci")]
        public bool EmailBookings { get; set; }

        [Display(Name = "Poslat e-mail, když někdo přidá novou zprávu na nástěnku")]
        public bool EmailMessages { get; set; }

        [Display(Name = "Nové heslo", Description = "Pokud nechcete změnit heslo, nechte pole prázdné.")]
        public string NewPassword { get; set; }

        [Display(Name = "Současné heslo pro potvrzení změny")]
        [Required(ErrorMessage = "Není zadáno současné heslo")]
        public string CurrentPassword { get; set; }


    }
}