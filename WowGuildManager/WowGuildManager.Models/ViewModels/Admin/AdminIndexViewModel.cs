using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Users;
using System.Linq;

namespace WowGuildManager.Models.ViewModels.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<UserAdminViewModel> Users { get; set; }

        public int UserCount => this.Users.Count();
    }
}
