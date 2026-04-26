using Microsoft.AspNetCore.Mvc;
using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.Entity.Concrete;
using blogWithAPI.Entity.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using blogWithAPI.Filters;


namespace blogWithAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Microsoft.AspNetCore.RateLimiting.EnableRateLimiting("strict")]
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
        [AuditLog(ActionName = "ADD_POST")]
        public IActionResult Add(Blog blog)
        {
            var result = _blogService.Add(blog);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        [AuditLog(ActionName = "UPDATE_POST")]
        public IActionResult Update(Blog blog)
        {
            var result = _blogService.Update(blog);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("{id}")]
        [AuditLog(ActionName = "DELETE_POST")]
        public IActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
