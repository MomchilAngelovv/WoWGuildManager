namespace WowGuildManager.Services.Api
{
    using System.Collections.Generic;

    public interface IApiService
    {
        IEnumerable<T> GetAll<T>(string userId);

        T GetCharacterById<T>(string characterId);

        IEnumerable<T> Members<T>();
    }
}
