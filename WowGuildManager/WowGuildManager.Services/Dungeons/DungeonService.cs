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
        private const string RfkImage = "/images/dungeons/rfkImg.jpg";
        private const string MaraImage = "/images/dungeons/maraImg.jpg";
        private const string RfdImage = "/images/dungeons/rfdImg.jpg";
        private const string DiremaulImage = "/images/dungeons/diremaulImg.jpg";
        private const string ScholoImage = "/images/dungeons/scholoImg.jpg";
        private const string UldaImage = "/images/dungeons/uldaImg.jpg";
        private const string StratImage = "/images/dungeons/stratImg.jpg";
        private const string ZfImage = "/images/dungeons/zfImg.jpg";
        private const string BrdImage = "/images/dungeons/brdImg.jpg";
        private const string StImage = "/images/dungeons/stImg.jpg";
        private const string LbrsImage = "/images/dungeons/lbrsImg.jpg";

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
                .Include(dungeon => dungeon.Leader)
                .Include(dungeon => dungeon.RegisteredCharacters)
                .ToList();
        }

        public Dungeon Create(DungeonCreateInputModel inputModel)
        {
            var dungeon = new Dungeon
            {
                DateTime = inputModel.DateTime,
                Description = inputModel.Description,
                Place = inputModel.Place,
                LeaderId = inputModel.DungeonLeaderId
            };

            this.SetDungeonImage(dungeon);

            dungeon.RegisteredCharacters.Add(new DungeonCharacter
            {
                CharacterId = dungeon.LeaderId,
                DungeonId = dungeon.Id
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

        private void SetDungeonImage(Dungeon dungeon)
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
                    dungeon.Image = RfkImage;
                    break;
                case DungeonPlace.MARA:
                    dungeon.Image = MaraImage;
                    break;
                case DungeonPlace.RFD:
                    dungeon.Image = RfdImage;
                    break;
                case DungeonPlace.DIREMAUL:
                    dungeon.Image = DiremaulImage;
                    break;
                case DungeonPlace.SCHOLO:
                    dungeon.Image = ScholoImage;
                    break;
                case DungeonPlace.ULDA:
                    dungeon.Image = UldaImage;
                    break;
                case DungeonPlace.STRAT:
                    dungeon.Image = StratImage;
                    break;
                case DungeonPlace.ZF:
                    dungeon.Image = ZfImage;
                    break;
                case DungeonPlace.BRD:
                    dungeon.Image = BrdImage;
                    break;
                case DungeonPlace.ST:
                    dungeon.Image = StImage;
                    break;
                case DungeonPlace.LBRS:
                    dungeon.Image = LbrsImage;
                    break;
            }
        }

        public void RegisterCharacter(string characterId, string dungeonId)
        {
            var dungeonCharacter = new DungeonCharacter
            {
                CharacterId = characterId,
                DungeonId = dungeonId
            };

            this.context.DungeonCharacter.Add(dungeonCharacter);
            this.context.SaveChanges();
        }
    }
}
