using Microsoft.AspNetCore.Mvc;
using Menu.Data;
using pizza.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;


namespace pizza.Controllers
{
    public class Menu : Controller
    {
        private readonly MenuContext _context;

        public Menu(MenuContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string SearchParams)
        {
            var dishes = from d in _context.Dishes
                       select d;
            if(!string.IsNullOrEmpty(SearchParams) )
            {
                dishes = dishes.Where(d => d.Name.Contains(SearchParams));
                return View(await dishes.ToListAsync());

            }
            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details( int id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredient)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);
            if( dish == null)
            {
                return NotFound();
            }
            return View(dish);
                
        }
    }
}
