using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.BindingModels.Guilds
{
    public class GuildIndexBindingModel
    {
        public IFormFile Image { get; set; }
    }
}
