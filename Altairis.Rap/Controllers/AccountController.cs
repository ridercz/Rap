using System;
using Altairis.Rap.Models;
using System.Web.Mvc;
using System.Web.Security;
using Altairis.Rap.Data;
using System.Linq;

namespace Altairis.Rap.Controllers {
    [RoutePrefix("account")]
    public class AccountController : Controller {

        private RapDbContext dc = new RapDbContext();

        [Route("")]
        public ActionResult Index() {
            var user = dc.Users.Single(x => x.UserName.Equals(this.User.Identity.Name));
            var model = new AccountViewModel {
                EmailAddress = user.Email,
                EmailBookings = user.EmailBookings,
                EmailMessages = user.EmailMessages
            };
            ViewBag.Title = "Moje nastavení";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Home";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route(""), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(AccountViewModel model) {
            if (this.TryUpdateModel(model)) {
                if (!Membership.ValidateUser(this.User.Identity.Name, model.CurrentPassword)) {
                    // Invalid password
                    this.ModelState.AddModelError("CurrentPassword", "Současné heslo je chybné");
                }
                else {
                    // Update e-mail
                    var membershipUser = Membership.GetUser();
                    membershipUser.Email = model.EmailAddress;
                    Membership.UpdateUser(membershipUser);

                    // Update password
                    if (!string.IsNullOrWhiteSpace(model.NewPassword)) membershipUser.ChangePassword(model.CurrentPassword, model.NewPassword);

                    // Update options
                    var user = dc.Users.Single(x => x.UserName.Equals(this.User.Identity.Name));
                    user.EmailBookings = model.EmailBookings;
                    user.EmailMessages= model.EmailMessages;
                    dc.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.Title = "Moje nastavení";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Home";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("logout")]
        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}