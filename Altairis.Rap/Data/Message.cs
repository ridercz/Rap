using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Altairis.Rap.Data {
    public class Message {

        [Key]
        public int MessageId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime DateCreated { get; set; }

        [MaxLength(50)]
        public string Subject { get; set; }

        public string Body { get; set; }
    }
}