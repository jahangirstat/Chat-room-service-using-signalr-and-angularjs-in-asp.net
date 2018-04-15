using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace chatService.Models
{
   public class MessageInfo
    {
        public int Id { get; set; }
        [Display(Name = "Message Body")]
        public string MessageBody { get; set; }
       [Display(Name = "Post DateTime")]
        public string PostDateTime { get; set; }
       [Display(Name = "User Name")]

        public string UserName { get; set; }
    }
}
