using blogWithAPI.BusinessLayer.Abstract;
using blogWithAPI.DataAccessLayer.Abstract;
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

        public blog Add(blog blog)
        {
            return _blogRepository.Add(blog);
        }

        public void Delete(int id)
        {
            _blogRepository.Delete(id);
        }

        public List<blog> GetAll()
        {
            return _blogRepository.GetAll();
        }

        public blog GetById(int id)
        {
            return _blogRepository.GetById(id);
        }

        public blog Update(blog blog)
        {
            return _blogRepository.Update(blog);
        }
    }
}