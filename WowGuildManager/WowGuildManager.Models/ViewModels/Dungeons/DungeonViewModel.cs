﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WowGuildManager.Models.ViewModels.Dungeons
{
    public class DungeonViewModel
    {
        public string Id { get; set; }

        public string DateTime { get; set; }

        public string Image { get; set; }

        public string Place { get; set; }

        public string Description { get; set; }

        public string DungeonLeaderName { get; set; }

        public string RegisteredPlayers { get; set; }

        public string MaxPlayers { get; set; }
    }
}