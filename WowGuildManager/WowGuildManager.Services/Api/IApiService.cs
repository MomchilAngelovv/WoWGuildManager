namespace WowGuildManager.Services.Api
{
    using System.Collections.Generic;

    public interface IApiService
    {
        IEnumerable<T> GetAllMembers<T>();
        IEnumerable<T> GetAllCharacters<T>(string userId);
        IEnumerable<T> GetAllImages<T>();
        IEnumerable<T> GetAllExceptions<T>();

        T GetCharacterById<T>(string characterId);

        IEnumerable<T> GuildProgress<T>();
    }
}
