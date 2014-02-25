using Altairis.Rap.Data;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Altairis.Rap.Controllers {

    [RoutePrefix("messages")]
    public class MessageController : Controller {
        private const int PAGE_SIZE = 10;
        private RapDbContext dc = new RapDbContext();

        [Route("{pageIndex:int:min(1)=1}")]
        public ActionResult Index(int pageIndex) {
            var offset = (pageIndex - 1) * PAGE_SIZE;
            var model = dc.Messages.Include("User").OrderByDescending(x => x.DateCreated).Skip(offset).Take(PAGE_SIZE);
            ViewBag.PageIndex = pageIndex;
            return View(model);
        }

        [Route("create")]
        public ActionResult Create() {
            ViewBag.Title = "Nová zpráva";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Messages";
            return View("~/Views/Shared/UniversalEditor.cshtml", new CreateMessageViewModel());
        }

        [Route("create"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(CreateMessageViewModel model) {
            if (this.TryUpdateModel(model)) {
                // Get ID of current user
                var userId = dc.Users.Single(x => x.UserName.Equals(this.User.Identity.Name)).UserId;

                // Create new message
                dc.Messages.Add(new Message {
                    Body = model.Body,
                    DateCreated = DateTime.Now,
                    Subject = model.Subject,
                    UserId = userId
                });
                dc.SaveChanges();

                // Send message
                if (RuntimeOptions.Current.UseMailing) {
                    var accountUri = new UriBuilder(this.Request.Url) { Path = this.Url.Action("Index", "Account"), Query = "", Fragment = "" };
                    var recipients = from u in dc.Users
                                     where u.UserId != userId && u.IsApproved && u.EmailMessages
                                     select u.Email;
                    Mailer.SendMail(recipients, Properties.Resources.MessageCreateSubject, Properties.Resources.MessageCreateBody,
                        model.Subject,              // 0
                        this.User.Identity.Name,    // 1
                        model.Body,                 // 2
                        accountUri);                // 3
                }

                return this.RedirectToAction("Index");
            }

            ViewBag.Title = "Nová zpráva";
            ViewBag.CancelAction = "Index";
            ViewBag.CancelController = "Messages";
            return View("~/Views/Shared/UniversalEditor.cshtml", model);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                dc.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}