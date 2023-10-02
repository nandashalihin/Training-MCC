﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_account_roles")]
    public class AccountRole : BaseEntity
    {
        [Column(name: "account_guid")]
        public Guid AccountGuid { get; set; }

        [Column(name: "role_guid")]
        public Guid RoleGuid { get; set; }

        public Role? Role { get; set; }
        public Account? Account { get; set; }
    }
}
