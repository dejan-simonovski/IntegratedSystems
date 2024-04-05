using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Models.DTO;

namespace MovieApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            if (userId != null)
            {
                var loggedInUser = await _context.Users
                    .Include(z => z.UserOrder)
                    .Include("UserOrder.TicketsInOrder")
                    .Include("UserOrder.TicketsInOrder.Ticket")
                    .Include("UserOrder.TicketsInOrder.Ticket.Movie")
                    .FirstOrDefaultAsync(z => z.Id == userId);

                var allTickets = loggedInUser?.UserOrder?.TicketsInOrder?.ToList();

                var totalPrice = 0.0;

                foreach (var item in allTickets)
                {
                    totalPrice += Double.Round((item.Quantity * item.Ticket.Price), 2);
                }

                var model = new OrderDTO
                {
                    AllTickets = allTickets,
                    TotalPrice = totalPrice
                };

                return View(model);

            }

            return View();

        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "UserName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "UserName", order.OwnerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "UserName", order.OwnerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<EShopApplicationUser>(), "Id", "UserName", order.OwnerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteTicketFromOrder(Guid? Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            if (userId != null)
            {
                 var loggedInUser = await _context.Users
                    .Include(z => z.UserOrder)
                    .Include("UserOrder.TicketsInOrder")
                    .FirstOrDefaultAsync(z => z.Id == userId);


                var product_to_delete = loggedInUser?.UserOrder.TicketsInOrder.First(z => z.TicketId == Id);

                loggedInUser?.UserOrder.TicketsInOrder.Remove(product_to_delete);

                _context.Orders.Update(loggedInUser?.UserOrder);
                _context.SaveChanges();

                return RedirectToAction("Index", "Orders");

            }
            return RedirectToAction("Index", "Orders");
        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

            if (userId != null)
            {
                var loggedInUser = await _context.Users
                        .Include(z => z.UserOrder)
                        .Include("UserOrder.TicketsInOrder")
                        .Include("UserOrder.TicketsInOrder.Ticket")
                        .FirstOrDefaultAsync(z => z.Id == userId);

                var userCart = loggedInUser?.UserOrder;

                var userOrder = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _context.Orders.Add(userOrder);
                _context.SaveChanges();

                var tickets = userCart?.TicketsInOrder.Select(z => new TicketInOrder
                {
                    Order = userOrder,
                    OrderId = userOrder.Id,
                    TicketId = z.TicketId,
                    Ticket = z.Ticket,
                    Quantity = z.Quantity
                }).ToList();

                if (tickets != null && tickets.Count > 0)
                {
                    _context.TicketInOrders.AddRange(tickets);
                    _context.SaveChanges();
                }

                if (loggedInUser.UserOrder.TicketsInOrder != null)
                {
                    loggedInUser?.UserOrder.TicketsInOrder.Clear();
                }

                _context.Orders.Update(loggedInUser?.UserOrder);
                _context.SaveChanges();

                return RedirectToAction("Index", "Orders");
            }
            return RedirectToAction("Index", "Tickets");
        }


        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
