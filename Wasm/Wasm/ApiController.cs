using Microsoft.AspNetCore.Mvc;

namespace Wasm
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Za Eric rules da universe!!!";
        }
    }
}