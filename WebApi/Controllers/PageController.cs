using App.Interfaces.Repository;
using App.Interfaces.Service;
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
        private readonly IPageService _pageService;

        public delegate Task<Choice[]> GetAllChoicesFromPage(int id);

        private readonly GetAllChoicesFromPage _getAllChoicesFromPage;

        public PageController(ILogger<StorieController> logger, IPageService pageService, GetAllChoicesFromPage getAllChoicesFromPage)
        {
            _logger = logger;
            _pageService = pageService;
            _getAllChoicesFromPage = getAllChoicesFromPage;

        }
        [HttpGet("Storie/{id}", Name = "GetAllFromStorie")]
        public async Task<IActionResult> GetAllFromStorie(int id)
        {
            Page[] stories = await _pageService.GetAllPageFromStorie(id);
            return Ok(stories);
        }
        [HttpGet("{id}", Name = "GetPageById")]
        public async Task<IActionResult> GetPageById(int id)
        {
            Page stories = await _pageService.GetPage(id);
            return Ok(stories);
        }        

        [HttpPost(Name = "CreatePage")]
        public async Task<IActionResult> CreatePage([FromBody] CreatePage inputPage)
        {
            Page page = await _pageService.CreatePage(inputPage);
            return CreatedAtRoute("GetPageById", new { id = page.Id }, page);
        }

        [HttpPatch("{id}", Name = "UpdatePage")]
        public async Task<IActionResult> UpdatePage(int id, [FromBody] UpdatePage inputPage)
        {            
            Page storie = await _pageService.UpdatePage(id, inputPage);
            return Ok(storie);            
        }

        [HttpDelete("{id}", Name = "DeletePage")]
        public async Task<IActionResult> DeletePage(int id)
        {            
            await _pageService.DeletePage(id);
            return Ok();            
        }

        [HttpPatch("Choice/add/{id}", Name = "AddChoices")]
        public async Task<IActionResult> AddChoices(int id, [FromBody] Choice[] choices)
        {
            Page storie = await _pageService.PageAddChoices(id, choices);
            return Ok(storie);
        }

        [HttpPatch("Choice/remove/{id}", Name = "RemoveChoices")]
        public async Task<IActionResult> RemoveChoices(int id, [FromBody] int[] choicesId)
        {
            Choice[] choices = await _getAllChoicesFromPage(id);

            Page storie = await _pageService.PageRemoveChoice(id, choices);
            return Ok(storie);
        }

    }
}
