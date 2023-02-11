using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIv2.Models;

namespace RestAPIv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly Kigul1897SelfstudyContext _context;

        public PurchasesController(Kigul1897SelfstudyContext context)
        {
            _context = context;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases()
        {
          if (_context.Purchases == null)
          {
              return NotFound();
          }
            return await _context.Purchases.ToListAsync();
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
          if (_context.Purchases == null)
          {
              return NotFound();
          }
            var purchase = await _context.Purchases.FindAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return purchase;
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(int id, Purchase purchase)
        {
            if (id != purchase.PruchaseId)
            {
                return BadRequest();
            }

            _context.Entry(purchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(Purchase purchase)
        {
          if (_context.Purchases == null)
          {
              return Problem("Entity set 'Kigul1897SelfstudyContext.Purchases'  is null.");
          }
            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchase", new { id = purchase.PruchaseId }, purchase);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            if (_context.Purchases == null)
            {
                return NotFound();
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Get Purchase by Session ID
        [HttpGet("purchaseHistory")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchases(int id)
        {
            var purchasequery = from purchase in _context.Purchases
                                join products in _context.Products on purchase.ProductId equals products.ProductId
                                join userinfo in _context.UserInfos on purchase.UserId equals userinfo.UserId
                                where purchase.UserId == TokenController.userId
                                select new
                                {
                                    purchase,
                                    products,
                                    userinfo.FirstName,
                                    userinfo.LastName,
                                    userinfo.Email
                                };

            if (_context.Purchases == null)
            {
                return NotFound();
            }

            if (purchasequery == null)
            {
                return NotFound();
            }

            var list = await purchasequery.ToListAsync();

            return Ok(list);
        }


        private bool PurchaseExists(int id)
        {
            return (_context.Purchases?.Any(e => e.PruchaseId == id)).GetValueOrDefault();
        }
    }
}
