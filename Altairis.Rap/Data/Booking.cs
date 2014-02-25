using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Data {
    public class Booking {

        [Key, ScaffoldColumn(false)]
        public int BookingId { get; set; }

        [Display(Name = "Zdroj")]
        public Resource Resource { get; set; }

        [Required(ErrorMessage = "Není zadán zdroj")]
        public int ResourceId { get; set; }

        [Display(Name = "Aktivita")]
        public Activity Activity { get; set; }

        [Required(ErrorMessage = "Není zadána aktivita")]
        public int ActivityId { get; set; }

        [Display(Name = "Uživatel")]
        public User User { get; set; }

        [Required(ErrorMessage = "Není zadán uživatel")]
        public int UserId { get; set; }

        [Display(Name = "Datum a čas začátku")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Není zadán začátek rezervace")]
        public DateTime DateBegin { get; set; }

        [Display(Name = "Datum a čas konce")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Není zadán konec rezervace")]
        public DateTime DateEnd { get; set; }

        [Display(Name = "Poznámka")]
        public string Notes { get; set; }

        public string DateString {
            get {
                if (this.DateBegin.Date.Equals(DateEnd.Date)) {
                    if (DateBegin.Date.Equals(DateTime.Today)) {
                        return string.Format("dnes, {0:t} - {1:t}", this.DateBegin, this.DateEnd);
                    }
                    else if (DateBegin.Date.AddDays(-1).Equals(DateTime.Today)) {
                        return string.Format("zítra, {0:t} - {1:t}", this.DateBegin, this.DateEnd);
                    }
                    else {
                        return string.Format("{0:d}, {0:t} - {1:t}", this.DateBegin, this.DateEnd);
                    }
                }
                else {
                    return string.Format("{0:d}, {0:t} - {1:d}, {1:t}", this.DateBegin, this.DateEnd);
                }
            }
        }

    }
}