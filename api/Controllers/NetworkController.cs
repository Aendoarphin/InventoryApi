using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : ControllerBase
    {
        private readonly INetworkService _network;

        public NetworkController(INetworkService network)
        {
            _network = network;
        }

        [HttpGet("ping/{ip}")]
        public async Task<IActionResult> GetPing(string ip)
        {
            var result = await _network.PingDevice(ip);
            return Ok(result);
        }
    }
}