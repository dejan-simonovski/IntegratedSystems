using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConcertApp.Web.Data;
using ConcertApp.Web.Models;

namespace ConcertApp.Web.Controllers
{
    public class ConcertTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConcertTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConcertTickets
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Ticket
                .Include(t => t.Concert)
                .Include(t => t.User)
                .ToListAsync();

            return View(tickets);
        }

        // GET: ConcertTickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Concert)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: ConcertTickets/Create
        public IActionResult Create()
        {
            ViewData["ConcertId"] = new SelectList(_context.Concert, "Id", "ConcertName");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: ConcertTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfPeople,UserId,ConcertId")] Ticket ticket)
        {
            if (ModelState.IsValid)
           {
                ticket.Id = Guid.NewGuid();
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConcertId"] = new SelectList(_context.Concert, "Id", "ConcertName", ticket.ConcertId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);
            return View(ticket);
        }

        // GET: ConcertTickets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ConcertId"] = new SelectList(_context.Concert, "Id", "ConcertName", ticket.ConcertId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);
            return View(ticket);
        }

        // POST: ConcertTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NumberOfPeople,UserId,ConcertId")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            ViewData["ConcertId"] = new SelectList(_context.Concert, "Id", "ConcertName", ticket.ConcertId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", ticket.UserId);
            return View(ticket);
        }

        // GET: ConcertTickets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Ticket
                .Include(t => t.Concert)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: ConcertTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket != null)
            {
                _context.Ticket.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
