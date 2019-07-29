namespace WowGuildManager.Services.Api
{
    using System.Collections.Generic;

    using WowGuildManager.Models.ApiModels.Characters;
    using WowGuildManager.Models.ApiModels.Logs;
    using WowGuildManager.Models.ApiModels.Raids;

    public interface IApiService
    {
        IEnumerable<CharacterApiViewModel> GetAllMembers();
        IEnumerable<CharacterApiViewModel> GetAllCharacters(string userId);
        IEnumerable<ImageApiViewModel> GetAllImages();
        IEnumerable<ExceptionApiViewModel> GetAllExceptions();

        CharacterApiViewModel GetCharacter(string characterId);

        IEnumerable<RaidDestinationProgressApiViewModel> GuildProgress();
    }
}
