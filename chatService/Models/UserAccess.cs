﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chatService.Models
{
   public class UserAccess
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string MenuItem { get; set; }
    }
}
