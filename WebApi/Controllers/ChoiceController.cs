using App.Interfaces.Repository;
using App.Models.DTO.Choices;
using Book.Models;
using Book.Models.DTO.Stories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ChoiceController : ControllerBase
    {

        private readonly ILogger<StorieController> _logger;
        private readonly IChoiceRepository _choiceRepo;

        public ChoiceController(ILogger<StorieController> logger, IChoiceRepository choiceRepo)
        {
            _logger = logger;
            _choiceRepo = choiceRepo;
        }

        [HttpGet("Page/{id}", Name = "GetAllChoicesFromPage")]
        public async Task<IActionResult> GetAllChoicesFromPage(int id)
        {
            IEnumerable<Choice> choice = await _choiceRepo.GetAllChoicesFromPage(id);
            return Ok(choice);
        }

        [HttpPost(Name = "CreateChoice")]
        public async Task<IActionResult> CreateChoice([FromBody] CreateChoice inputChoice)
        {
            Choice choice = await _choiceRepo.CreateChoice(inputChoice);
            return CreatedAtRoute("GetStorieById", new { id = choice.Id }, choice);
        }

        [HttpPatch("{id}", Name = "UpdateChoice")]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] UpdateChoice inputChoice)
        {
            Choice choice = await _choiceRepo.UpdateChoice(id, inputChoice);
            return Ok(choice);
        }

        [HttpDelete("{id}", Name = "DeleteChoice")]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            await _choiceRepo.DeleteChoice(id);
            return Ok();
        }

    }
}
