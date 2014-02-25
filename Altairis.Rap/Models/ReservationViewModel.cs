using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Models {
    public class ReservationViewModel : IValidatableObject {

        [Display(Name = "Datum"), DataType(DataType.Date)]
        [Required(ErrorMessage = "Není zadáno datum")]
        public DateTime Date { get; set; }

        [Display(Name = "Čas začátku"), DataType(DataType.Time)]
        [Required(ErrorMessage = "Není zadán začátek rezervace")]
        public TimeSpan TimeBegin { get; set; }

        [Display(Name = "Čas konce"), DataType(DataType.Time)]
        [Required(ErrorMessage = "Není zadán konec rezervace")]
        public TimeSpan TimeEnd { get; set; }

        [Display(Name = "Aktivita"), Required(ErrorMessage = "Není zadána aktivita")]
        public int ActivityId { get; set; }

        public IEnumerable<SelectListItem> ActivityList { get; set; }

        [Display(Name = "Poznámka")]
        public string Notes { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateBegin {
            get {
                return this.Date.Add(this.TimeBegin);
            }
        }

        [ScaffoldColumn(false)]
        public DateTime DateEnd {
            get {
                return this.Date.Add(this.TimeEnd);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (this.Date < DateTime.Today) yield return new ValidationResult("Rezervace nesmí začínat v minulosti", new[] { "Date" });
            if (this.TimeEnd <= TimeBegin) yield return new ValidationResult("Rezervace nesmí končit dříve, než začínat", new[] { "TimeEnd" });
        }
    }
}