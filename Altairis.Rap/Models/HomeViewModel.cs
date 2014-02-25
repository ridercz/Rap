using Altairis.Rap.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Models {
    public class HomeViewModel {

        public int MyBookingCount { get; set; }

        public IEnumerable<Resource> Resources { get; set; }

        public IEnumerable<Message> Messages { get; set; }

    }
}