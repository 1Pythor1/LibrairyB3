using App.Interfaces.Service;
using Book.Models;
using Book.Models.DTO.Stories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorieController : ControllerBase
    {

        private readonly ILogger<StorieController> _logger;
        private readonly  IStorieService _storieService;

        public StorieController(ILogger<StorieController> logger, IStorieService storieService)
        {
            _logger = logger;
            _storieService = storieService;
        }

        [HttpGet(Name = "GetAllStorie")]
        public async Task<IActionResult> GetAllStorie()
        {
            IEnumerable<Storie>  stories = await _storieService.GetAllStories();
            return Ok(stories);
        }            

        [HttpGet("{id}", Name = "GetStorieById")]
        public async Task<IActionResult> GetStorieById(int id) 
        {
            Storie storie = await _storieService.GetStorie(id);
            return Ok(storie);
        }            

        [HttpPost(Name = "CreateStorie")]
        public async Task<IActionResult> CreateStorie([FromBody] CreateStorie inputStorie)
        {
            Storie storie = await _storieService.CreateStorie(inputStorie);
            return CreatedAtRoute("GetStorieById", new { id = storie.Id }, storie);
        }

        [HttpPatch("{id}", Name = "UpdateStorie")]
        public async Task<IActionResult> UpdateStorie(int id, [FromBody] UpdateStorie inputStorie)
        {
            Storie storie = await _storieService.UpdateStorie(id, inputStorie);
            return Ok(storie);
        }

        [HttpDelete("{id}", Name = "DeleteStorie")]
        public async Task<IActionResult> DeleteStorie(int id)
        {
            await _storieService.DeleteStorie(id);
            return Ok();
        }

    }
}
