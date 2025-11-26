using App.Interfaces.Repository;
using App.Models.DTO.Pages;
using Book.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class PageController : ControllerBase
    {

        private readonly ILogger<StorieController> _logger;
        private readonly IPageRepository _pageRepo;

        public delegate Task<Choice[]> GetAllChoicesFromPage(int id);

        private readonly GetAllChoicesFromPage _getAllChoicesFromPage;

        public PageController(ILogger<StorieController> logger, IPageRepository pageRepo, GetAllChoicesFromPage getAllChoicesFromPage)
        {
            _logger = logger;
            _pageRepo = pageRepo;
            _getAllChoicesFromPage = getAllChoicesFromPage;

        }
        [HttpGet("Storie/{id}", Name = "GetAllFromStorie")]
        public async Task<IActionResult> GetAllFromStorie(int id)
        {
            Page[] stories = await _pageRepo.GetAllPageFromStorie(id);
            return Ok(stories);
        }
        [HttpGet("{id}", Name = "GetPageById")]
        public async Task<IActionResult> GetPageById(int id)
        {
            Page stories = await _pageRepo.GetPage(id);
            return Ok(stories);
        }        

        [HttpPost(Name = "CreatePage")]
        public async Task<IActionResult> CreatePage([FromBody] CreatePage inputPage)
        {
            Page page = await _pageRepo.CreatePage(inputPage);
            return CreatedAtRoute("GetPageById", new { id = page.Id }, page);
        }

        [HttpPatch("{id}", Name = "UpdatePage")]
        public async Task<IActionResult> UpdatePage(int id, [FromBody] UpdatePage inputPage)
        {            
            Page storie = await _pageRepo.UpdatePage(id, inputPage);
            return Ok(storie);            
        }

        [HttpDelete("{id}", Name = "DeletePage")]
        public async Task<IActionResult> DeletePage(int id)
        {            
            await _pageRepo.DeletePage(id);
            return Ok();            
        }

        [HttpPatch("Choice/add/{id}", Name = "AddChoices")]
        public async Task<IActionResult> AddChoices(int id, [FromBody] Choice[] choices)
        {
            Page storie = await _pageRepo.PageAddChoices(id, choices);
            return Ok(storie);
        }

        [HttpPatch("Choice/remove/{id}", Name = "RemoveChoices")]
        public async Task<IActionResult> RemoveChoices(int id, [FromBody] int[] choicesId)
        {
            Choice[] choices = await _getAllChoicesFromPage(id);

            Page storie = await _pageRepo.PageRemoveChoice(id, choices);
            return Ok(storie);
        }

    }
}
