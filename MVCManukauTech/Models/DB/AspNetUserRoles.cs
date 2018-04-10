﻿using System;
using System.Collections.Generic;

namespace MVCManukauTech.Models.DB
{
    public partial class AspNetUserRoles
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool? SilverMembership { get; set; }
        public bool? GoldMembership { get; set; }
        public bool? NoMembership { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
