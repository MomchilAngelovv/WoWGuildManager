using System;
using System.Collections.Generic;
using System.Text;
using WowGuildManager.Domain.Characters;

namespace WowGuildManager.Services.Api
{
    public interface IApiService
    {
        IEnumerable<Character> GetAll(string userId);

        Character GetCharacterById(string characterId);
    }
}
