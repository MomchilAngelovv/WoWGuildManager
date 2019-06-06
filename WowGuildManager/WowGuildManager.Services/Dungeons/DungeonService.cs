using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Data;
using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Services.Characters;

namespace WowGuildManager.Services.Dungeons
{
    public class DungeonService : IDungeonService
    {
        private const string RfcImage = "/images/rfcImg.jpg";

        private readonly WowGuildManagerDbContext context;
        private readonly ICharacterService characterService;

        public DungeonService(
            WowGuildManagerDbContext context,
            ICharacterService characterService)
        {
            this.context = context;
            this.characterService = characterService;
        }

        public Dungeon Create(DungeonCreateViewModel inputModel)
        {
            var dungeonLeader = this.characterService.GetCharacterById(inputModel.DungeonLeaderId);

            var dungeon = new Dungeon
            {
                DateTime = inputModel.DateTime,
                DungeonLeader = dungeonLeader,
                Description = inputModel.Description,
                Place = inputModel.Place,
                LeaderId = inputModel.DungeonLeaderId
            };

            this.SetNewDungeonImage(dungeon);

            dungeon.RegisteredCharacters.Add(new DungeonCharacters
            {
                Character = dungeonLeader,
                Dungeon = dungeon
            });

            this.context.Dungeons.Add(dungeon);
            this.context.SaveChanges();

            return dungeon;
        }

        public IEnumerable<DungeonPlace> GetPlaces()
        {
            return Enum.GetValues(typeof(DungeonPlace))
                .Cast<DungeonPlace>()
                .ToList();
        }

        private void SetNewDungeonImage(Dungeon dungeon)
        {
            switch (dungeon.Place)
            {
                case DungeonPlace.RFC:
                    dungeon.Image = RfcImage;
                    break;
                case DungeonPlace.WC:
                    break;
                case DungeonPlace.DM:
                    break;
                case DungeonPlace.SFK:
                    break;
                case DungeonPlace.BFD:
                    break;
                case DungeonPlace.STOCKS:
                    break;
                case DungeonPlace.GNOME:
                    break;
                case DungeonPlace.SM:
                    break;
                case DungeonPlace.RFK:
                    break;
                case DungeonPlace.MARA:
                    break;
                case DungeonPlace.RFD:
                    break;
                case DungeonPlace.DIREM:
                    break;
                case DungeonPlace.SCHOLO:
                    break;
                case DungeonPlace.ULDA:
                    break;
                case DungeonPlace.STRAT:
                    break;
                case DungeonPlace.ZF:
                    break;
                case DungeonPlace.BRD:
                    break;
                case DungeonPlace.ST:
                    break;
                case DungeonPlace.LBRS:
                    break;
            }
        }
    }
}
