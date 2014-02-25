using Altairis.Rap.Data;
using Altairis.Rap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;

namespace Altairis.Rap {
    public static class Mailer {
        public static void SendMail(string recipient, string subject, string body, params object[] args) {
            SendMail(new[] { recipient }, subject, body, args);
        }

        public static void SendMail(IEnumerable<string> recipients, string subject, string body, params object[] args) {
            // Validate arguments
            if (recipients == null) throw new ArgumentNullException("recipients");
            if (subject == null) throw new ArgumentNullException("subject");
            if (string.IsNullOrWhiteSpace(subject)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "subject");
            if (body == null) throw new ArgumentNullException("body");
            if (string.IsNullOrWhiteSpace(body)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "body");

            if (!recipients.Any()) return;                  // nobody to send mail to
            if (!RuntimeOptions.Current.UseMailing) return; // mailing is disabled

            // Compose message
            using (var msg = new MailMessage()) {
                msg.From = new MailAddress(Membership.GetUser("administrator", false).Email, RuntimeOptions.Current.ApplicationTitle);
                msg.Subject = string.Format(subject, args);
                msg.Body = string.Format(body, args);

                // Setup mailer
                using (var mx = new SmtpClient()) {
                    if (string.IsNullOrWhiteSpace(RuntimeOptions.Current.SmtpHostName)) {
                        mx.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        mx.PickupDirectoryLocation = @"C:\InetPub\MailRoot\pickup";
                    }
                    else {
                        mx.DeliveryMethod = SmtpDeliveryMethod.Network;
                        mx.Host = RuntimeOptions.Current.SmtpHostName;
                        mx.Credentials = new System.Net.NetworkCredential(RuntimeOptions.Current.SmtpUserName, RuntimeOptions.Current.SmtpPassword);
                    }

                    // Send messages
                    foreach (var recipient in recipients) {
                        msg.To.Clear();
                        msg.To.Add(recipient);
                        mx.Send(msg);
                    }
                }
            }

        }


    }
}