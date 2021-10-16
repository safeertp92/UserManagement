﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Liwapoi.DB.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
