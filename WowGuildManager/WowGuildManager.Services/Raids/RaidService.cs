using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowGuildManager.Data;
using WowGuildManager.Domain.Dungeon;
using WowGuildManager.Domain.Raid;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Characters;

namespace WowGuildManager.Services.Raids
{
    public class RaidService : IRaidService
    {
        private const string UbrsImage = "/images/raids/ubrsImg.jpg";
        private const string Aq20Image = "/images/raids/aq20Img.jpg";
        private const string Aq40Image = "/images/raids/aq40Img.jpg";
        private const string BwlImage = "/images/raids/bwlImg.jpg";
        private const string McImage = "/images/raids/mcImg.jpg";
        private const string NaxxImage = "/images/raids/naxxImg.jpg";
        private const string OnyImage = "/images/raids/onyImg.jpg";
        private const string ZgImage = "/images/raids/zgImg.jpg";

        private readonly ICharacterService characterService;
        private readonly WowGuildManagerDbContext context;

        public RaidService(ICharacterService characterService, WowGuildManagerDbContext context)
        {
            this.characterService = characterService;
            this.context = context;
        }
        public Raid Create(RaidCreateInputModel inputModel)
        {
            var raid = new Raid
            {
                DateTime = inputModel.DateTime,
                Description = inputModel.Description,
                Place = inputModel.Place,
                LeaderId = inputModel.RaidLeaderId,
            };

            this.SetRaidImageAndMaxPlayers(raid);

            raid.RegisteredCharacters.Add(new RaidCharacter
            {
                CharacterId = inputModel.RaidLeaderId,
                Raid = raid
            });

            this.context.Raids.Add(raid);
            this.context.SaveChanges();

            return raid;
        }

        private void SetRaidImageAndMaxPlayers(Raid raid)
        {
            switch (raid.Place)
            {
                case RaidPlace.UBRS:
                    raid.Image = UbrsImage;
                    raid.MaxPlayers = 10;
                    break;
                case RaidPlace.ZG:
                    raid.Image = ZgImage;
                    raid.MaxPlayers = 20;
                    break;
                case RaidPlace.AQ20:
                    raid.Image = Aq20Image;
                    raid.MaxPlayers = 20;
                    break;
                case RaidPlace.MC:
                    raid.Image = McImage;
                    raid.MaxPlayers = 40;
                    break;
                case RaidPlace.BWL:
                    raid.Image = BwlImage;
                    raid.MaxPlayers = 40;
                    break;
                case RaidPlace.ONY:
                    raid.Image = OnyImage;
                    raid.MaxPlayers = 40;
                    break;
                case RaidPlace.AQ40:
                    raid.Image = Aq40Image;
                    raid.MaxPlayers = 40;
                    break;
                case RaidPlace.NAXX:
                    raid.Image = NaxxImage;
                    raid.MaxPlayers = 40;
                    break;
            }
        }

        public IQueryable<Raid> GetAll()
        {
            return this.context.Raids
               .Include(raid => raid.Leader)
               .Include(raid => raid.RegisteredCharacters);
        }

        public IQueryable<RaidPlace> GetPlaces()
        {
            return Enum.GetValues(typeof(RaidPlace)).Cast<RaidPlace>().AsQueryable();
        }
    }
}
