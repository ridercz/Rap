using Altairis.Rap.Areas.Admin.Models;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Areas.Admin.Controllers {
    [RouteArea("admin")]
    [RoutePrefix("options")]
    [Authorize(Users = "administrator")]
    public class OptionsController : Controller {

        [Route("")]
        public ActionResult Index() {
            ViewBag.Title = "Nastavení systému";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Home";
            ViewBag.HeaderText = "<b>Pozor!</b> Nesprávné nastavení může způsobit nefunkčnost aplikace. Pokud nevíte, co děláte, neměňte hodnoty na této stránce.";
            return View("~/Views/Shared/UniversalEditor.cshtml", RuntimeOptions.Current);
        }

        [Route(""), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(RuntimeOptions model) {
            if (TryUpdateModel(model)) {
                model.Save();
                return this.RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.Title = "Nastavení systému";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Home";
            ViewBag.HeaderText = "Pozor! Nesprávné nastavení může způsobit nefunkčnost aplikace. Pokud nevíte, co děláte, neměňte hodnoty na této stránce.";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }
    }
}