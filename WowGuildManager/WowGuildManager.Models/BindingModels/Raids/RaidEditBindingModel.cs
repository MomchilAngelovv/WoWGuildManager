﻿namespace WowGuildManager.Models.BindingModels.Raids
{
    using System;

    public class RaidEditBindingModel
    {
        public string RaidId { get; set; }

        public string Description { get; set; }

        public DateTime? EventDateTime { get; set; }
    }
}
