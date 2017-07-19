using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using ProductKeyServer.Models;

namespace ProductKeyServer.Controllers.api
{
    public class ServerController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ServerController()
        {
            _context = new ApplicationDbContext();
        }
        
        public IHttpActionResult Get(string productKey, string hardwareId)
        {
            var key = _context.Keys.FirstOrDefault(k => k.ProductKey == productKey && k.HardwareId == hardwareId);

            if (key == null) { return BadRequest(); }

            if (key.IsDisabled || DateTime.Now > key.ExpirationDate) { return BadRequest(); }

            key.LastChecked = DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }
        
        public IHttpActionResult Post(string productKey, string hardwareId)
        {
            var key = _context.Keys.FirstOrDefault(k => k.HardwareId != hardwareId && k.ProductKey == productKey);
            if (key == null) return BadRequest();
            // create new product key, return OK

            key.HardwareId = hardwareId;
            key.ExpirationDate = DateTime.Now.AddDays(3);
            key.LastChecked = DateTime.Now;

            _context.SaveChanges();

            return Ok();
        }

        public IHttpActionResult Put(string productKey, string hardwareId, DateTime expirationDate)
        {
            Key key = _context.Keys.FirstOrDefault(k => k.HardwareId == hardwareId && k.ProductKey == productKey);
            if (key == null){
                return BadRequest();
            }
            key.ExpirationDate = expirationDate;
            _context.SaveChanges();
            return Ok();
        }

        public IHttpActionResult Delete(string productKey, string hardwareId)
        {
            Key key = _context.Keys.FirstOrDefault(k => k.ProductKey == productKey && k.HardwareId == hardwareId);
            if (key == null) return NotFound();
            key.IsDisabled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
