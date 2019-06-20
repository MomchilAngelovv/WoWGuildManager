using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;

namespace WowGuildManager.Services.Raids
{
    public interface IRaidService
    {
        IQueryable<Raid> GetAll();

        IQueryable<RaidPlace> GetPlaces();

        Raid Create(RaidCreateInputModel inputModel);
    }
}
