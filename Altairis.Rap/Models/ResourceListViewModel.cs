using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Models {
    public class ResourceListViewModel {

        public string ResourceName { get; set; }

        public string ResourceDescription { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }

        public class Booking {

            public string UserName { get; set; }

            public string UserEmail { get; set; }

            public DateTime DateBegin { get; set; }

            public DateTime DateEnd { get; set; }

            public string Notes { get; set; }

            public string ActivityName { get; set; }

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
}