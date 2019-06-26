using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Users;

namespace WowGuildManager.Models.ViewModels.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<UserAdminViewModel> Users { get; set; }
    }
}
