using Altairis.Rap.Data;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Controllers {
    public class BookingController : Controller {
        RapDbContext dc = new RapDbContext();

        [Route("{resourceId:int}")]
        public ActionResult Index(int resourceId) {
            var r = dc.Resources.SingleOrDefault(x => x.ResourceId == resourceId);
            if (r == null) return this.HttpNotFound();

            var model = new ResourceListViewModel {
                ResourceName = r.Name,
                ResourceDescription = r.Description,
                Bookings = from b in dc.Bookings
                           where b.ResourceId == r.ResourceId && b.DateBegin >= DateTime.Today
                           orderby b.DateBegin
                           select new ResourceListViewModel.Booking {
                               DateBegin = b.DateBegin,
                               DateEnd = b.DateEnd,
                               UserName = b.User.UserName,
                               UserEmail = b.User.Email,
                               Notes = b.Notes,
                               ActivityName = b.Activity.Name
                           },
            };

            ViewBag.HistoryView = false;
            return View(model);
        }

        [Route("{resourceId:int}/history")]
        public ActionResult History(int resourceId) {
            var r = dc.Resources.SingleOrDefault(x => x.ResourceId == resourceId);
            if (r == null) return this.HttpNotFound();

            var model = new ResourceListViewModel {
                ResourceName = r.Name,
                ResourceDescription = r.Description,
                Bookings = from b in dc.Bookings
                           where b.ResourceId == r.ResourceId
                           orderby b.DateBegin descending
                           select new ResourceListViewModel.Booking {
                               DateBegin = b.DateBegin,
                               DateEnd = b.DateEnd,
                               UserName = b.User.UserName,
                               UserEmail = b.User.Email,
                               Notes = b.Notes,
                               ActivityName = b.Activity.Name
                           },
            };

            ViewBag.HistoryView = true;
            return View("Index", model);
        }

        [Route("mybookings")]
        public ActionResult MyBookings() {
            var model = from b in dc.Bookings.Include("Resource").Include("Activity")
                        where b.User.UserName.Equals(this.User.Identity.Name) && b.DateBegin >= DateTime.Today
                        orderby b.DateBegin
                        select b;
            ViewBag.Title = "Moje rezervace";
            return this.View(model);
        }

        [Route("mybookings/{bookingid:int}/delete")]
        public ActionResult Delete(int bookingId) {
            var b = dc.Bookings.Include("Resource").SingleOrDefault(x => x.BookingId == bookingId && x.User.UserName.Equals(this.User.Identity.Name));
            if (b == null) return this.HttpNotFound();

            ViewBag.Title = "Smazání rezervace";
            ViewBag.ConfirmPrompt = string.Format("Opravdu chcete zrušit rezervaci \"{0}\" v termínu {1}?", b.Resource.Name, b.DateString);
            ViewBag.CancelAction = "MyBookings";
            return View("~/Views/Shared/UniversalConfirmation.cshtml");
        }

        [Route("mybookings/{bookingid:int}/delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int bookingId, object fake) {
            var b = dc.Bookings.SingleOrDefault(x => x.BookingId == bookingId && x.User.UserName.Equals(this.User.Identity.Name));
            if (b == null) return this.HttpNotFound();

            dc.Bookings.Remove(b);
            dc.SaveChanges();

            return this.RedirectToAction("MyBookings");
        }

        [Route("{resourceId:int}/create")]
        public ActionResult Create(int resourceId) {
            var model = new ReservationViewModel {
                ActivityList = dc.Activities.OrderBy(x => x.Name).AsEnumerable().Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.ActivityId.ToString()
                })
            };
            return this.View(model);
        }

        [Route("{resourceId:int}/create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(int resourceId, ReservationViewModel model) {
            if (this.ModelState.IsValid) {
                // Get ID of current user
                var userId = dc.Users.Single(x => x.UserName.Equals(this.User.Identity.Name)).UserId;

                // Check if there is some reservation for given date
                var taken = dc.Bookings.Any(x => x.ResourceId == resourceId && (x.DateBegin >= model.DateBegin && x.DateBegin < model.DateEnd) || (x.DateEnd > model.DateBegin && x.DateEnd <= model.DateEnd) || (x.DateBegin <= model.DateBegin && x.DateEnd >= model.DateEnd));
                if (!taken) {
                    var newBooking = new Booking {
                        DateBegin = model.DateBegin,
                        DateEnd = model.DateEnd,
                        ResourceId = resourceId,
                        ActivityId = model.ActivityId,
                        Notes = model.Notes,
                        UserId = userId
                    };
                    dc.Bookings.Add(newBooking);
                    dc.SaveChanges();

                    // Send notification
                    if (RuntimeOptions.Current.UseMailing) {
                        var accountUri = new UriBuilder(this.Request.Url) { Path = this.Url.Action("Index", "Account"), Query = "", Fragment = "" };
                        var recipients = from u in dc.Users
                                         where u.UserId != userId && u.IsApproved && u.EmailBookings
                                         select u.Email;
                        var resourceName = dc.Resources.Single(x => x.ResourceId == resourceId).Name;
                        Mailer.SendMail(recipients, Properties.Resources.BookingCreateSubject, Properties.Resources.BookingCreateBody,
                            this.User.Identity.Name,    // 0
                            resourceName,               // 1
                            newBooking.DateString,      // 2
                            accountUri);                // 3
                    }

                    return this.RedirectToAction("Index", new { resourceId = resourceId });
                }
                ModelState.AddModelError(string.Empty, "Ve vybraném časovém úseku již je jiná rezervace");
            }
            model.ActivityList = dc.Activities.OrderBy(x => x.Name).AsEnumerable().Select(x => new SelectListItem {
                Text = x.Name,
                Value = x.ActivityId.ToString()
            });
            return this.View(model);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) dc.Dispose();
            base.Dispose(disposing);
        }
    }
}