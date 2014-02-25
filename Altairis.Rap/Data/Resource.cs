using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Data {
    public class Resource {

        [Key, ScaffoldColumn(false)]
        public int ResourceId { get; set; }

        [Display(Name = "Název")]
        [Required(ErrorMessage = "Není zadán název")]
        [MaxLength(50, ErrorMessage = "{0} smí mít nejvýše {1} znaků")]
        public string Name { get; set; }

        [Display(Name = "Popis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

    }
}