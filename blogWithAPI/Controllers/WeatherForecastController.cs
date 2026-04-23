using Microsoft.AspNetCore.Mvc;

namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public WeatherForecastController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_blogService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_blogService.GetById(id));
        }

        [HttpPost]
        public IActionResult Add(blog blog)
        {
            _blogService.Add(blog);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(blog blog)
        {
            _blogService.Update(blog);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blogService.Delete(id);
            return Ok();
        }
    }
}
