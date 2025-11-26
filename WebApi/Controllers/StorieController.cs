using App.Interfaces.Repository;
using Book.Error;
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
        private readonly  IStorieRepository _storieRepo;

        public StorieController(ILogger<StorieController> logger, IStorieRepository storieRepo)
        {
            _logger = logger;
            _storieRepo = storieRepo;
        }

        [HttpGet(Name = "GetAllStorie")]
        public async Task<IActionResult> GetAllStorie()
        {
            IEnumerable<Storie>  stories = await _storieRepo.GetAllStories();
            return Ok(stories);
        }            

        [HttpGet("{id}", Name = "GetStorieById")]
        public async Task<IActionResult> GetStorieById(int id) 
        {
            Storie storie = await _storieRepo.GetStorie(id);
            return Ok(storie);
        }            

        [HttpPost(Name = "CreateStorie")]
        public async Task<IActionResult> CreateStorie([FromBody] CreateStorie inputStorie)
        {
            Storie storie = await _storieRepo.CreateStorie(inputStorie);
            return CreatedAtRoute("GetStorieById", new { id = storie.Id }, storie);
        }

        [HttpPatch("{id}", Name = "UpdateStorie")]
        public async Task<IActionResult> UpdateStorie(int id, [FromBody] UpdateStorie inputStorie)
        {
            Storie storie = await _storieRepo.UpdateStorie(id, inputStorie);
            return Ok(storie);
        }

        [HttpDelete("{id}", Name = "DeleteStorie")]
        public async Task<IActionResult> DeleteStorie(int id)
        {
            await _storieRepo.DeleteStorie(id);
            return Ok();
        }

    }
}
