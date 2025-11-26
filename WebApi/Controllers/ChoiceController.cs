using App.Interfaces.Repository;
using App.Interfaces.Service;
using App.Models.DTO.Choices;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class ChoiceController : ControllerBase
    {

        private readonly ILogger<StorieController> _logger;
        private readonly IChoiceService _choiceService;

        public ChoiceController(ILogger<StorieController> logger, IChoiceService choiceService)
        {
            _logger = logger;
            _choiceService = choiceService;
        }

        [HttpGet("Page/{id}", Name = "GetAllChoicesFromPage")]
        public async Task<IActionResult> GetAllChoicesFromPage(int id)
        {
            IEnumerable<Choice> choice = await _choiceService.GetAllChoicesFromPage(id);
            return Ok(choice);
        }

        [HttpPost(Name = "CreateChoice")]
        public async Task<IActionResult> CreateChoice([FromBody] CreateChoice inputChoice)
        {
            Choice choice = await _choiceService.CreateChoice(inputChoice);
            return CreatedAtRoute("GetStorieById", new { id = choice.Id }, choice);
        }

        [HttpPatch("{id}", Name = "UpdateChoice")]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] UpdateChoice inputChoice)
        {
            Choice choice = await _choiceService.UpdateChoice(id, inputChoice);
            return Ok(choice);
        }

        [HttpDelete("{id}", Name = "DeleteChoice")]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            await _choiceService.DeleteChoice(id);
            return Ok();
        }

    }
}
