using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Altairis.Rap.Data {
    public class User {
        [Key, ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Není zadáno uživatelské jméno")]
        [MaxLength(100)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Není zadán e-mail")]
        [MaxLength(200)]
        public string Email { get; set; }

        public bool EmailBookings { get; set; }

        public bool EmailMessages { get; set; }

        #region Required by membership provider
        
        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }

        [MaxLength(200)]
        public string Comment { get; set; }

        public bool IsApproved { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastLogin { get; set; }

        public DateTime? DateLastActivity { get; set; }

        public DateTime DateLastPasswordChange { get; set; }

        #endregion

        public virtual ICollection<Booking> Bookings { get; set; }

    }

}
