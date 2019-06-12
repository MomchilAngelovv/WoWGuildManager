using Microsoft.EntityFrameworkCore;
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
        private const string RfcImage = "/images/dungeons/rfcImg.jpg";
        private const string WcImage = "/images/dungeons/wcImg.jpg";
        private const string DmImage = "/images/dungeons/dmImg.jpg";
        private const string SfkImage = "/images/dungeons/sfkImg.jpg";
        private const string BfdImage = "/images/dungeons/bdfImg.jpg";
        private const string StocksImage = "/images/dungeons/stocksImg.jpg";
        private const string GnomeImage = "/images/dungeons/gnomeImg.jpg";
        private const string SmImage = "/images/dungeons/smImg.jpg";


        private readonly WowGuildManagerDbContext context;
        private readonly ICharacterService characterService;

        public DungeonService(
            WowGuildManagerDbContext context,
            ICharacterService characterService)
        {
            this.context = context;
            this.characterService = characterService;
        }

        public IEnumerable<Dungeon> GetAll()
        {
            return this.context.Dungeons
                .Include(dungeon => dungeon.DungeonLeader)
                .Include(dungeon => dungeon.RegisteredCharacters)
                .ToList();
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
                    dungeon.Image = WcImage;
                    break;
                case DungeonPlace.DM:
                    dungeon.Image = DmImage;
                    break;
                case DungeonPlace.SFK:
                    dungeon.Image = SfkImage;
                    break;
                case DungeonPlace.BFD:
                    dungeon.Image = BfdImage;
                    break;
                case DungeonPlace.STOCKS:
                    dungeon.Image = StocksImage;
                    break;
                case DungeonPlace.GNOME:
                    dungeon.Image = GnomeImage;
                    break;
                case DungeonPlace.SM:
                    dungeon.Image = SmImage;
                    break;
                case DungeonPlace.RFK:
                    break;
                case DungeonPlace.MARA:
                    break;
                case DungeonPlace.RFD:
                    break;
                case DungeonPlace.DIREMAUL:
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
