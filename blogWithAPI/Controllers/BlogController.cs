using Microsoft.AspNetCore.Mvc;
using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using Microsoft.AspNetCore.Authorization;


namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
           var result = _blogService.GetAll();
           if(result.IsSuccess)
           {
            return Ok(result);
           }
           return BadRequest(result);
        }

        [HttpGet("{id}")]
        [Route("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _blogService.GetById(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public IActionResult Add(blog blog)
        {
            var result = _blogService.Add(blog);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPut]
        [Route("update")]
        public IActionResult Update(blog blog)
        {
            var result = _blogService.Update(blog);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
