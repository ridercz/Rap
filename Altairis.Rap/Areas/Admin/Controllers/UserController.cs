using Altairis.Rap.Areas.Admin.Models;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Altairis.Rap.Areas.Admin.Controllers {
    [RouteArea("admin")]
    [RoutePrefix("users")]
    [Authorize(Users = "administrator")]
    public class UserController : Controller {

        [Route("")]
        public ActionResult Index() {
            var model = Membership.GetAllUsers().Cast<MembershipUser>();
            return View(model);
        }

        [Route("create")]
        public ActionResult Create() {
            var model = new UserCreateViewModel {
                Password = Membership.GeneratePassword(12, 1)
            };
            if (RuntimeOptions.Current.UseMailing) {
                ViewBag.HeaderText = "Na zadanou e-mailovou adresu bude automaticky zaslána zpráva s instrukcemi pro přihlášení.";
            }
            ViewBag.Title = "Nový uživatel";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateViewModel model) {
            if (this.TryUpdateModel(model)) {
                MembershipCreateStatus status;
                var newUser = Membership.CreateUser(model.UserName, model.Password, model.EmailAddress, null, null, true, out status);
                if (status == MembershipCreateStatus.Success) {
                    var homeUri = new UriBuilder(this.Request.Url) { Path = "", Query = "", Fragment = "" };
                    Mailer.SendMail(model.EmailAddress, Properties.Resources.UserCreateSubject, Properties.Resources.UserCreateBody,
                        RuntimeOptions.Current.ApplicationTitle,
                        homeUri,
                        model.UserName,
                        model.Password);
                    return this.RedirectToAction("Index");
                }
                this.ModelState.AddModelError(null, string.Format("Nepodařilo se vytvořit uživatele: {0}", status));
            }
            ViewBag.Title = "Nový uživatel";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{userName}")]
        public ActionResult Edit(string userName) {
            var user = Membership.GetUser(userName, false);
            if (user == null) return this.HttpNotFound();
            var model = new UserEditViewModel {
                EmailAddress = user.Email,
                IsApproved = user.IsApproved,
            };
            ViewBag.Title = string.Format("Editace uživatele '{0}'", user.UserName);
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{userName}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel model, string userName) {
            var user = Membership.GetUser(userName, false);
            if (user == null) return this.HttpNotFound();
            if (this.TryUpdateModel(model)) {
                user.Email = model.EmailAddress;
                user.IsApproved = model.IsApproved;
                Membership.UpdateUser(user);
                if (!string.IsNullOrWhiteSpace(model.NewPassword)) {
                    var tempPassword = user.ResetPassword();
                    user.ChangePassword(tempPassword, model.NewPassword);
                }
                return this.RedirectToAction("Index");
            }
            ViewBag.Title = string.Format("Editace uživatele '{0}'", user.UserName);
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

    }
}