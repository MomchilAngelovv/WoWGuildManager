namespace WowGuildManager.Models.ViewModels.Admin
{
    using System.Collections.Generic;
    using System.Linq;

    using WowGuildManager.Models.ViewModels.Users;
    public class AdminIndexViewModel
    {
        public IEnumerable<UserAdminViewModel> Users { get; set; }

        public int UserCount => this.Users.Count();
    }
}
