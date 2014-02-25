using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Models {
    public class CreateMessageViewModel {

        [Display(Name = "Nadpis")]
        [Required(ErrorMessage = "Není zadán nadpis zprávy"), MaxLength(50, ErrorMessage = "Nadpis zprávy může mít nejvýše 50 znaků")]
        public string Subject { get; set; }

        [Display(Name = "Text"), DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Není zadán text zprávy")]
        public string Body { get; set; }

    }
}