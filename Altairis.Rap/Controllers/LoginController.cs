using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Altairis.Rap.Controllers {
    [RoutePrefix("login")]
    public class LoginController : Controller {

        [Route("")]
        public ActionResult Index(string returnUrl) {
            if (!RuntimeOptions.Current.SetupCompleted) return RedirectToAction("Setup");

            ViewBag.Title = "Přihlášení";
            ViewBag.HeaderText = "Pro vstup do systému se musíte přihlásit. Zadejte prosím své uživatelské jméno a heslo.";
            return View("~/Views/Shared/UniversalEditor.cshtml", new LoginViewModel());
        }
        
        [Route(""), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model, string returnUrl) {
            if (!RuntimeOptions.Current.SetupCompleted) return RedirectToAction("Setup");

            if (this.TryUpdateModel(model)) {
                if (Membership.ValidateUser(model.UserName, model.Password)) {
                    FormsAuthentication.RedirectFromLoginPage(model.UserName.ToLower(), model.RememberMe);
                }
                else {
                    ModelState.AddModelError(string.Empty, "Chybné uživatelské jméno nebo heslo");
                    model.Password = null;
                }
            }

            ViewBag.Title = "Přihlášení";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("setup")]
        public ActionResult Setup() {
            if (RuntimeOptions.Current.SetupCompleted) return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);

            ViewBag.Title = "První spuštění";
            return View("~/Views/Shared/UniversalEditor.cshtml", new SetupViewModel());
        }
        
        [Route("setup"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Setup(SetupViewModel model) {
            if (RuntimeOptions.Current.SetupCompleted) return new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);

            if (this.TryUpdateModel(model)) {
                model.InitializeApplicationData();
                return RedirectToAction("Index");
            }
            ViewBag.Title = "První spuštění";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);

        }

    }
}