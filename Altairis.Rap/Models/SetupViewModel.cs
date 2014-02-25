using Altairis.Rap.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Altairis.Rap.Models {
    public class SetupViewModel {

        [Display(Name = "Název aplikace", Prompt = "Registrační systém RAP", Description="Zobrazuje se v titulku, např. 'JK Epona'.")]
        [Required(ErrorMessage = "Není zadán název")]
        public string ApplicationTitle { get; set; }

        [Display(Name = "Nové heslo správce", Description = "Heslo speciálního uživatele 'Administrator', který smí zakládat nové uživatele, zdroje a nastavovat vlastnosti systému.")]
        [Required(ErrorMessage = "Není zadáno nové heslo správce")]
        public string AdministratorPassword { get; set; }

        [Display(Name = "E-mail správce"), DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Není zadán e-mail správce")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Zadaný e-mail správce je chybný")]
        public string AdministratorEmail { get; set; }

        [Display(Name = "Název prvního zdroje", Description = "Např. 'Jezdecká hala'. Další zdroje budete moci přidat později.")]
        [Required(ErrorMessage = "Není zadán název prvního zdroje")]
        [MaxLength(50, ErrorMessage = "{0} smí mít nejvýše {1} znaků")]
        public string FirstResourceName { get; set; }

        public void InitializeApplicationData() {
            // Set runtime options; this also creates ~/App_Data/ folder
            RuntimeOptions.Current.ApplicationTitle = this.ApplicationTitle;
            RuntimeOptions.Current.Save();

            // Create new resource; this also creates the database 
            using (var dc = new RapDbContext()) {
                dc.Resources.Add(new Resource { Name = this.FirstResourceName });
                dc.SaveChanges();
            }

            // Update parameters of user 'Administrator'
            var admin = Membership.GetUser("Administrator", false);
            var tempPassword = admin.ResetPassword();
            admin.ChangePassword(tempPassword, this.AdministratorPassword);
            admin.Email = this.AdministratorEmail;
            Membership.UpdateUser(admin);
        }

    }
}