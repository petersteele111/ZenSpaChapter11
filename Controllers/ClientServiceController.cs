using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPages.Models;

namespace RazorPages.Controllers
{
    public class ClientServiceController : Controller
    {
        private readonly SpaContext _context;

        public ClientServiceController(SpaContext context)
        {
            _context = context;
        }

        // GET: ClientService
        public async Task<IActionResult> Index()
        {
            var spaContext = _context.ClientServices.Include(c => c.Client).Include(c => c.Services);
            return View(await spaContext.ToListAsync());
        }

        // GET: ClientService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientServices
                .Include(c => c.Client)
                .Include(c => c.Services)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clientService == null)
            {
                return NotFound();
            }

            return View(clientService);
        }

        // GET: ClientService/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Email");
            ViewData["ServicesID"] = new SelectList(_context.Services, "ID", "ID");
            return View();
        }

        // POST: ClientService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClientID,ServicesID,Date")] ClientService clientService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Email", clientService.ClientID);
            ViewData["ServicesID"] = new SelectList(_context.Services, "ID", "ID", clientService.ServicesID);
            return View(clientService);
        }

        // GET: ClientService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientServices.FindAsync(id);
            if (clientService == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Email", clientService.ClientID);
            ViewData["ServicesID"] = new SelectList(_context.Services, "ID", "ID", clientService.ServicesID);
            return View(clientService);
        }

        // POST: ClientService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClientID,ServicesID,Date")] ClientService clientService)
        {
            if (id != clientService.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientServiceExists(clientService.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ID", "Email", clientService.ClientID);
            ViewData["ServicesID"] = new SelectList(_context.Services, "ID", "ID", clientService.ServicesID);
            return View(clientService);
        }

        // GET: ClientService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientService = await _context.ClientServices
                .Include(c => c.Client)
                .Include(c => c.Services)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (clientService == null)
            {
                return NotFound();
            }

            return View(clientService);
        }

        // POST: ClientService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientService = await _context.ClientServices.FindAsync(id);
            _context.ClientServices.Remove(clientService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientServiceExists(int id)
        {
            return _context.ClientServices.Any(e => e.ID == id);
        }
    }
}
