using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GatewaysApi.Models;

namespace GatewaysApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly GatewayDBContext _context;

        public GatewaysController(GatewayDBContext context)
        {
            _context = context;
        }

        // GET: api/Gateways
        [HttpGet]
        [Route("GetAllGateways")]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateway()
        {
            return await _context.Gateway.Where(c => true).Include("PeripheralDevice").ToListAsync();           
        }

        // GET: api/Gateways/5
        [HttpGet]
        [Route("GetGatewayById")]
        public async Task<ActionResult<Gateway>> GetGateway(int id)
        {
            var gateway = await _context.Gateway.FindAsync(id);
            var devices = _context.PeripheralDevice.Where(c => c.GatewayId == id).ToList();
            gateway.PeripheralDevice = devices;

            if (gateway == null)
            {
                return NotFound();
            }

            return gateway;
        }

        [HttpPut]
        [Route("UpdateGateway")]
        public async Task<IActionResult> PutGateway(int id, Gateway gateway)
        {
            if (id != gateway.Id)
            {
                return BadRequest();
            }

             var devicesCount = _context.PeripheralDevice.Where(a => a.GatewayId == id).Count();
            if (gateway.PeripheralDevice.Count > 10 || devicesCount > 10)
            {
                return BadRequest("the number of peripheral devices allowed is 10 only");
            }

            _context.Entry(gateway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayExists(id))
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

        [HttpPost]
        [Route("AddGateway")]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
          if( gateway.PeripheralDevice.Count > 10)
            {
                return BadRequest( "the number of peripheral devices allowed is 10 only");
            }
            _context.Gateway.Add(gateway);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = gateway.Id }, gateway);
        }

        // DELETE: api/Gateways/5
        [HttpDelete]
        [Route("DeleteGateway")]
        public async Task<ActionResult<Gateway>> DeleteGateway(int id)
        {
            var gateway = await _context.Gateway.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateway.Remove(gateway);
            await _context.SaveChangesAsync();

            return gateway;
        }
        private bool GatewayExists(int id)
        {
            return _context.Gateway.Any(e => e.Id == id);
        }
    }
}
