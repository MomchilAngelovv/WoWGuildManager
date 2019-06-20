using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Services.Api
{
    public interface IApiService
    {
        IQueryable<T> GetAll<T>(string userId);

        T GetCharacterById<T>(string characterId);
    }
}
