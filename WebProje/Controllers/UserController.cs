// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WebProje.Data;
// using WebProje.Models;
//
// namespace WebProje.Controllers
// {
//     public class UserController : Controller
//     {
//         private readonly ApplicationContext _context;
//
//         public UserController(ApplicationContext context)
//         {
//             _context = context;
//         }
//
//         // GET: User
//         public async Task<IActionResult> Index()
//         {
//             return _context.Users != null
//                 ? View(await _context.Users.ToListAsync())
//                 : Problem("Entity set 'ApplicationContext.Users'  is null.");
//         }
//
//         // GET: User/Details/5
//         public async Task<IActionResult> Details(string? id)
//         {
//             if (id == null || _context.Users == null)
//             {
//                 return NotFound();
//             }
//
//             var user = await _context.Users
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//
//             return View(user);
//         }
//
//         // GET: User/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: User/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("Name,Password")] User user)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(user);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//             {
//                 Console.WriteLine(error.ErrorMessage);
//             }
//
//
//             return View(user);
//         }
//
//         // GET: User/Edit/5
//         public async Task<IActionResult> Edit(int? id)
//         {
//             if (id == null || _context.Users == null)
//             {
//                 return NotFound();
//             }
//
//             var user = await _context.Users.FindAsync(id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//
//             return View(user);
//         }
//
//         // POST: User/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Password")] User user)
//         {
//             if (id != user.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(user);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!UserExists(user.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//
//                 return RedirectToAction(nameof(Index));
//             }
//
//             return View(user);
//         }
//
//         // GET: User/Delete/5
//         public async Task<IActionResult> Delete(string? id)
//         {
//             if (id == null || _context.Users == null)
//             {
//                 return NotFound();
//             }
//
//             var user = await _context.Users
//                 .FirstOrDefaultAsync(m => m.Id == id);
//             if (user == null)
//             {
//                 return NotFound();
//             }
//
//             return View(user);
//         }
//
//         // POST: User/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(int id)
//         {
//             if (_context.Users == null)
//             {
//                 return Problem("Entity set 'ApplicationContext.Users'  is null.");
//             }
//
//             var user = await _context.Users.FindAsync(id);
//             if (user != null)
//             {
//                 _context.Users.Remove(user);
//             }
//
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//
//         private bool UserExists(string id)
//         {
//             return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
//         }
//     }
// }