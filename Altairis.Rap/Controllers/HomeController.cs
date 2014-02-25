using Altairis.Rap.Data;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Controllers {
    public class HomeController : Controller {
        [Route("~/")]
        public ActionResult Index() {
            using (var dc = new RapDbContext()) {
                var model = new HomeViewModel {
                    MyBookingCount = dc.Bookings.Count(x => x.User.UserName.Equals(this.User.Identity.Name) && x.DateBegin >= DateTime.Today),
                    Resources = dc.Resources.OrderBy(x => x.Name).ToArray(),
                    Messages = dc.Messages.Include("User").OrderByDescending(x => x.DateCreated).Take(3).ToArray()
                };
                return View(model);
            }
        }

    }
}