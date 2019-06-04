using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WowGuildManager.Domain.Raid
{
    public enum RaidPlace
    {
        [Display(Name = "Zul'Gubub")] ZG = 1,
        [Display(Name = "Ruins of Ahn'Qiraj")] AQ20 = 2,
        [Display(Name = "Molten Core")] MC = 3,
        [Display(Name = "Blackwing Lair")] BWL = 4,
        [Display(Name = "Onyxia's Lair")] ONY = 5,
        [Display(Name = "The Temple of Ahn'Qiraj")] AQ40 = 6,
        [Display(Name = "Naxxramas")] NAXX = 7
    }
}
