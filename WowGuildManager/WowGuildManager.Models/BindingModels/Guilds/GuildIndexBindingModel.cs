using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Models.ViewModels.Guild;

namespace WowGuildManager.Models.BindingModels.Guilds
{
    public class GuildMasterViewModel
    {
        public IFormFile Image { get; set; }
    }
}
