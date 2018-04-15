using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace chatService.Models
{
   public class GroupInfo
    {
        public int Id { get; set; }
       [Display(Name = "Group Name")]
        public string GroupName { get; set; }
    }
}
