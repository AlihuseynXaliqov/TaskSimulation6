using AutoMapper;
using Azure.Core.GeoJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSimulation6.Areas.Manage.Helpers.Exception;
using TaskSimulation6.Areas.Manage.ViewModel.Agent;
using TaskSimulation6.Areas.Manage.ViewModel.Position;
using TaskSimulation6.DAL.Context;
using TaskSimulation6.Helpers;
using TaskSimulation6.Models;

namespace TaskSimulation6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AgentController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment web;

        public AgentController(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment web)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.web = web;
        }

        public async Task<IActionResult> Index()
        {
            var agents = await dbContext.Agents.Include(x => x.Position).ToListAsync();
            return View(agents);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Position = await dbContext.Positions.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAgentVm vm)
        {
            ViewBag.Position = await dbContext.Positions.ToListAsync();
            if (!ModelState.IsValid) return View(vm);
            if (!vm.formFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                return View(vm);
            }
            if (vm.formFile.Length > 2097152)
            {
                ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                return View(vm);
            }
            vm.ImageUrl = vm.formFile.Upload(web.WebRootPath, "Upload/Agent");

            var agent = mapper.Map<Agent>(vm);
            await dbContext.Agents.AddAsync(agent);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Position = await dbContext.Positions.ToListAsync();

            if (id <= 0) throw new NegativeIdException();
            var oldAgent = await dbContext.Agents.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == id);
            if (oldAgent == null) throw new NotFoundException();
            var newAgent = mapper.Map<UpdateAgentVM>(oldAgent);
            return View(newAgent);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAgentVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            ViewBag.Position = await dbContext.Positions.ToListAsync();
            var oldAgent = await dbContext.Agents.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == vm.Id);

            if (oldAgent == null) throw new NotFoundException();

            if (vm.formFile != null)
            {
                if (!vm.formFile.ContentType.Contains("image"))
                {
                    ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                    return View(vm);
                }
                if (vm.formFile.Length > 2097152)
                {
                    ModelState.AddModelError("formFile", "Sekil duzgun deyil");
                    return View(vm);
                }
                if (string.IsNullOrEmpty(oldAgent.ImageUrl))
                {
                    FileExtention.Delete(web.WebRootPath, "Upload/Agent", oldAgent.ImageUrl);
                }
                oldAgent.ImageUrl = vm.formFile.Upload(web.WebRootPath, "Upload/Agent");


            }
            oldAgent.Name = vm.Name;
            oldAgent.PositionId = vm.PositionId;

            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var oldAgent = await dbContext.Agents.Include(x => x.Position).FirstOrDefaultAsync(x => x.Id == id);
            if (oldAgent == null) throw new NotFoundException();
            dbContext.Agents.Remove(oldAgent);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
