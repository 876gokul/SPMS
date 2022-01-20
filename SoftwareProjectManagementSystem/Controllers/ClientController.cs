using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareProjectManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareProjectManagementSystem.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Project Manager")]
    public class ClientController : Controller
    {
        private readonly testContext db;

        public ClientController(testContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchBy, string search, int pageNumber = 1)
        {
            var clients = HelperClass.ClientListWithInclude(db);
            if (searchBy == "CompanyName")
            {
                clients = clients.Where(c => c.CompanyName.StartsWith(search) || search == null);
            }
            else
            {
                clients = clients.Where(c => c.Name.StartsWith(search) || search == null);
            }
            return base.View(await PagingList<Client>.CreateAsync(clients, pageNumber, 4));
        }

        // GET: Client/Create
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Client/Details/5

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var client = await HelperClass.ClientWithInclude(db, (int)id);
            if (client == null) return NotFound();
            return View(client);
        }

        // GET: Client/Edit/5

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)  return NotFound();
            var client = await HelperClass.ClientWithInclude(db, (int)id);
            if (client == null) return NotFound();
            return View(client);
        }

        // POST: Client/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Client client)
        {
            if (id != client.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    db.Clients.Update(client);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var client = await HelperClass.ClientWithInclude(db, (int)id);
            if (client == null)  return NotFound();
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await HelperClass.ClientWithInclude(db, (int)id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Any(e => e.Id == id);
        }

    }
}
