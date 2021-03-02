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
    public class PeripheralDevicesController : ControllerBase
    {
        private readonly GatewayDBContext _context;

        public PeripheralDevicesController(GatewayDBContext context)
        {
            _context = context;
        }

        // GET: api/PeripheralDevices
        [HttpGet]
        [Route("GetAllDevices")]
        public async Task<ActionResult<IEnumerable<PeripheralDevice>>> GetPeripheralDevice()
        {
            return await _context.PeripheralDevice.ToListAsync();
        }

        // GET: api/PeripheralDevices/5
        [HttpGet]
        [Route("GetDeviceById")]
        public async Task<ActionResult<PeripheralDevice>> GetPeripheralDevice(int id)
        {
            var peripheralDevice = await _context.PeripheralDevice.FindAsync(id);

            if (peripheralDevice == null)
            {
                return NotFound();
            }

            return peripheralDevice;
        }
     
        [HttpPut]
        [Route("UpdateDevice")]
        public async Task<IActionResult> PutPeripheralDevice(int id, PeripheralDevice peripheralDevice)
        {
            if (id != peripheralDevice.Id)
            {
                return BadRequest();
            }

            var devicesCount = _context.PeripheralDevice.Where(a => a.GatewayId == peripheralDevice.GatewayId).Count();
            if ( devicesCount > 10)
            {
                return BadRequest("the number of peripheral devices allowed is 10 only");
            }

            _context.Entry(peripheralDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheralDeviceExists(id))
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
        [Route("AddDevice")]
        public async Task<ActionResult<PeripheralDevice>> PostPeripheralDevice(PeripheralDevice peripheralDevice)
        {
            var devicesCount = _context.PeripheralDevice.Where(a => a.GatewayId == peripheralDevice.GatewayId).Count();
            if (devicesCount > 10)
            {
                return BadRequest("the number of peripheral devices allowed is 10 only");
            }
            _context.PeripheralDevice.Add(peripheralDevice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeripheralDevice", new { id = peripheralDevice.Id }, peripheralDevice);
        }

        // DELETE: api/PeripheralDevices/5
        [HttpDelete]
        [Route("DeleteDevice")]
        public async Task<ActionResult<PeripheralDevice>> DeletePeripheralDevice(int id)
        {
            var peripheralDevice = await _context.PeripheralDevice.FindAsync(id);
            if (peripheralDevice == null)
            {
                return NotFound();
            }

            _context.PeripheralDevice.Remove(peripheralDevice);
            await _context.SaveChangesAsync();

            return peripheralDevice;
        }

        private bool PeripheralDeviceExists(int id)
        {
            return _context.PeripheralDevice.Any(e => e.Id == id);
        }
    }
}
