using Microsoft.AspNetCore.Mvc;
using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
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
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _blogService.GetById(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public IActionResult Add(Blog blog)
        {
            var result = _blogService.Add(blog);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public IActionResult Update(Blog blog)
        {
            var result = _blogService.Update(blog);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
