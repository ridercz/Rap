using Altairis.Rap.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Areas.Admin.Controllers {
    [RouteArea("admin")]
    [RoutePrefix("resources")]
    [Authorize(Users = "administrator")]
    public class ResourceController : Controller {
        RapDbContext dc = new RapDbContext();

        [Route("")]
        public ActionResult Index() {
            var model = dc.Resources.OrderBy(x => x.Name);
            return View(model);
        }

        [Route("{resourceId:int}")]
        public ActionResult Edit(int resourceId) {
            var model = dc.Resources.SingleOrDefault(x => x.ResourceId == resourceId);
            if (model == null) return this.HttpNotFound();

            ViewBag.Title = "Editace zdroje";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{resourceId:int}"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Resource model) {
            if (this.ModelState.IsValid) {
                dc.Entry(model).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return this.RedirectToAction("Index");
            }
            ViewBag.Title = "Editace zdroje";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        [Route("{resourceId:int}/delete")]
        public ActionResult Delete(int resourceId) {
            ViewBag.Title = "Smazání zdroje";
            ViewBag.ConfirmPrompt = "Pokud smažete zdroj, budou nevratně smazány všechny údaje, které s ním souvisejí. Opravdu chcete tento zdroj smazat?";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalConfirmation.cshtml");
        }

        [Route("{resourceId:int}/delete"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int resourceId, object fake) {
            dc.Resources.Remove(dc.Resources.SingleOrDefault(x => x.ResourceId == resourceId));
            dc.SaveChanges();
            return this.RedirectToAction("Index");
        }

        [Route("create")]
        public ActionResult Create() {
            ViewBag.Title = "Nový zdroj";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", new Resource());
        }

        [Route("create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Resource model) {
            if (this.ModelState.IsValid) {
                dc.Resources.Add(model);
                dc.SaveChanges();
                return this.RedirectToAction("Index");
            }
            ViewBag.Title = "Nový zdroj";
            ViewBag.CancelAction = "Index";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }
        
        protected override void Dispose(bool disposing) {
            if (disposing) dc.Dispose();
            base.Dispose(disposing);
        }

    }
}