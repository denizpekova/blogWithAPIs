using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.DataAccessLayer.Abstract;
using blogWithAPI.Entity.Results;
using blogWithAPI.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogWithAPI.BusinessLayer.Concrate
{
    public class BlogManager : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogManager(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public IResult Add(blog blog)
        {
            _blogRepository.Add(blog);
            return new SuccessResult("added successfully");
        }

        public IResult Delete(int id)
        {
            _blogRepository.Delete(id);
            return new SuccessResult("deleted successfully");
        }   

        public IDataResult<List<blog>> GetAll()
        {
            return new SuccessDataResult<List<blog>>(_blogRepository.GetAll(), "listed successfully");
        }

        public IDataResult<blog> GetById(int id)
        {
            return new SuccessDataResult<blog>(_blogRepository.GetById(id), "listed successfully");
        }

        public IResult Update(blog blog)
        {
            _blogRepository.Update(blog);
            return new SuccessResult("updated successfully");
        }
    }
}