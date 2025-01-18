using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskSimulation6.Areas.Manage.Helpers.Exception;
using TaskSimulation6.Areas.Manage.ViewModel.Position;
using TaskSimulation6.DAL.Context;
using TaskSimulation6.Models;

namespace TaskSimulation6.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;

        public PositionController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var positions = await dbContext.Positions.Include(x => x.Agents).ToListAsync();
            return View(positions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionVm vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (!await dbContext.Positions.AnyAsync(x => x.Name == vm.Name))
            {
                var position = mapper.Map<Position>(vm);
                await dbContext.Positions.AddAsync(position);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            var oldPosition = await dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (oldPosition == null) throw new NotFoundException();
            var newPosition = mapper.Map<UpdatePositionVM>(oldPosition);
            return View(newPosition);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePositionVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var position = await dbContext.Positions.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (position == null) throw new NotFoundException();
            position.Name = vm.Name;
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var oldPosition = await dbContext.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (oldPosition == null) throw new NotFoundException();
            dbContext.Positions.Remove(oldPosition);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}