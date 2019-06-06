using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WowGuildManager.Models.ViewModels.Dungeons;
using WowGuildManager.Models.ViewModels.Events;
using WowGuildManager.Services.Dungeons;

namespace WowGuildManager.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IDungeonService dungeonService;
        private readonly IMapper mapper;

        public EventsController(
            IDungeonService dungeonService,
            IMapper mapper)
        {
            this.dungeonService = dungeonService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var dungeons = this.dungeonService.GetAll()
                .Select(dungeon => mapper.Map<DungeonViewModel>(dungeon));

            var eventsIndexViewModel = new EventsIndexViewModel
            {
                Dungeons = dungeons
            };

            return View(eventsIndexViewModel);
        }
    }
}