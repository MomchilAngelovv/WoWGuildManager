using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Services.Raids
{
    public interface IRaidService
    {
        IEnumerable<Raid> GetAll();

        IEnumerable<RaidPlace> GetPlaces();

        Raid Create(RaidCreateInputModel inputModel);
    }
}
