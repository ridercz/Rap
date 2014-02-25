using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Data {
    public class Activity {

        [Key, ScaffoldColumn(false)]
        public int ActivityId { get; set; }

        [Display(Name = "Název")]
        [Required(ErrorMessage = "Není zadán název"), MaxLength(50, ErrorMessage = "{0} smí mít nejvýše {1} znaků")]
        public string Name { get; set; }

    }
}