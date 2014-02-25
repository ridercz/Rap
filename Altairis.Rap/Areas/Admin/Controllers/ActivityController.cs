using Altairis.Rap.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Areas.Admin.Controllers {
    [RouteArea("admin")]
    [RoutePrefix("activities")]
    [Authorize(Users = "administrator")]
    public class ActivityController : Controller {
        RapDbContext dc = new RapDbContext();

        [Route("")]
        public ActionResult Index() {
            var model = dc.Activities.OrderBy(x => x.Name);
            return View(model);
        }

        [Route("{activityId:int}")]
        public ActionResult Edit(int activityId) {
            var model = dc.Activities.SingleOrDefault(x => x.ActivityId == activityId);
            if (model == null) return this.HttpNotFound();

            ViewBag.Title = "Editace aktivity";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{activityId:int}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Activity model) {
            if (this.ModelState.IsValid) {
                dc.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return this.RedirectToAction("Index");
            }
            ViewBag.Title = "Editace aktivity";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{activityId:int}/delete")]
        public ActionResult Delete(int activityId) {
            ViewBag.Title = "Smazání aktivity";
            ViewBag.ConfirmPrompt = "Pokud smažete aktivitu, budou nevratně smazány všechny údaje, které s ní souvisejí, včetně rezervací. Opravdu chcete tuto aktivitu smazat?";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalConfirmation.cshtml");
        }

        [Route("{activityId:int}/delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int activityId, object fake) {
            dc.Activities.Remove(dc.Activities.SingleOrDefault(x => x.ActivityId == activityId));
            dc.SaveChanges();
            return this.RedirectToAction("Index");
        }

        [Route("create")]
        public ActionResult Create() {
            ViewBag.Title = "Nová aktivita";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", new Activity());
        }

        [Route("create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Activity model) {
            if (this.ModelState.IsValid) {
                dc.Activities.Add(model);
                dc.SaveChanges();
                return this.RedirectToAction("Index");
            }
            ViewBag.Title = "Nová aktivita";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) dc.Dispose();
            base.Dispose(disposing);
        }
    }
}