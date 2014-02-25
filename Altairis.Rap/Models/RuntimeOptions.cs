using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace Altairis.Rap.Models {
    public class RuntimeOptions {
        private const string FILE_NAME = "~/App_Data/RuntimeOptions.json";

        private static RuntimeOptions currentOptions;
        private static object readLock = new object();
        private static object writeLock = new object();

        [ScaffoldColumn(false)]
        public bool SetupCompleted { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateSaved { get; set; }

        [Display(Name = "Název aplikace", Description = "Zobrazuje se v titulku, např. 'JK Epona'.")]
        [Required(ErrorMessage = "Není zadán název")]
        public string ApplicationTitle { get; set; }

        [Display(Name = "Posílat e-maily", Description = "Zaškrtněte, pokud má aplikace posílat e-mailem oznámení o rezervacích a nových zprávách. Zprávy budou odesílány z e-mailové adresy uživatele administrator.")]
        public bool UseMailing { get; set; }

        [Display(Name = "SMTP server", Description = "Nevyplňujte, pokud nechcete posílat e-maily, nebo pokud chcete použít Mail Pickup Service.")]
        public string SmtpHostName { get; set; }

        [Display(Name = "SMTP port"), Range(1, 10000, ErrorMessage = "SMTP port musí být číslo od 1 do 10000")]
        public int SmtpPort { get; set; }

        [Display(Name = "SMTP uživatel")]
        public string SmtpUserName { get; set; }

        [Display(Name = "SMTP heslo")]
        public string SmtpPassword { get; set; }

        [ScaffoldColumn(false)]
        public static RuntimeOptions Current {
            get {
                if (currentOptions == null) {
                    lock (readLock) {
                        if (currentOptions == null) {
                            var jsonFileName = HttpContext.Current.Server.MapPath(FILE_NAME);
                            if (System.IO.File.Exists(jsonFileName)) {
                                // Load settings
                                var json = File.ReadAllText(jsonFileName);
                                currentOptions = JsonConvert.DeserializeObject<RuntimeOptions>(json);
                            }
                            else {
                                // Create default settings
                                currentOptions = new RuntimeOptions {
                                    ApplicationTitle = "Rezervační systém RAT",
                                    UseMailing = false,
                                    SmtpPort = 25,
                                };
                                currentOptions.Save(false);
                            }
                        }
                    }
                }
                return currentOptions;
            }
        }

        public void Save(bool setupCompleted = true) {
            var jsonFileName = HttpContext.Current.Server.MapPath(FILE_NAME);
            lock (writeLock) {
                currentOptions = this;
                currentOptions.SetupCompleted = setupCompleted;
                currentOptions.DateSaved = DateTime.Now;
                var json = JsonConvert.SerializeObject(currentOptions);
                File.WriteAllText(jsonFileName, json);
            }
        }

    }
}