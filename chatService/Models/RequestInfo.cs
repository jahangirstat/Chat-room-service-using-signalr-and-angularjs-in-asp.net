using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace chatService.Models
{
   public class RequestInfo
    {
        public int Id { get; set; }
       [Display(Name = "Group Name")]
        public string GroupName { get; set; }
       [Display(Name = "Request Date Time")]
        public string ReqDateTime { get; set; }
       [Display(Name = "User Name")]
        public string UserName { get; set; }
        public bool Approved { get; set; }
    }
}
