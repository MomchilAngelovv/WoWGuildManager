using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Events;
using WowGuildManager.Models.ViewModels.Raids;
using WowGuildManager.Services.Dungeons;
using WowGuildManager.Services.Raids;

namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IDungeonService dungeonService;
        private readonly IRaidService raidService;
        private readonly IMapper mapper;

        public EventsController(
            IDungeonService dungeonService,
            IRaidService raidService,
            IMapper mapper)
        {
            this.dungeonService = dungeonService;
            this.raidService = raidService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var dungeons = this.dungeonService.GetAll()
                .Select(dungeon => mapper.Map<DungeonViewModel>(dungeon));

            var raids = this.raidService.GetAll()
               .Select(raid => mapper.Map<RaidViewModel>(raid));

            var eventsIndexViewModel = new EventsIndexViewModel
            {
                Dungeons = dungeons,
                Raids = raids
            };

            return View(eventsIndexViewModel);
        }
    }
}