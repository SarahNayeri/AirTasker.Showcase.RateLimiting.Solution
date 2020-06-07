using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirTasker.Showcase.RateLimiting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWordController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Successfull access");
        }
    }
}