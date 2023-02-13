using BookStoresAPI.DataAccess;
using BookStoresAPI.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BookStoresAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PressController : ODataController
    {
        private readonly IPressRepositories _pressRepositories;

        public PressController(IPressRepositories pressRepositories)
        {
            _pressRepositories = pressRepositories;
        }

        [HttpGet]
        [Route("GetAll")]
        [EnableQuery]
        public async Task<IActionResult> GetAllBook()
        {
            var presses = await _pressRepositories.GetAllPress();
            return Ok(presses);
        }
    }
}
