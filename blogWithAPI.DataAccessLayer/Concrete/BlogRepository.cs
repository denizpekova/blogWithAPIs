using DataAccessLayer.Concrete;
using DataAccessLayerr.Abstrack;
using EntityLayerr.Concrate;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blogWithAPI.DataAccessLayer.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        private readonly Context _context;

        public BlogRepository(Context context)
        {
            _context = context;
        }

        public void Add(blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog;
        }

        public List<blog> GetAll()
        {
            return _context.Blogs.ToList();
        }

        public blog GetById(int id)
        {
            return _context.Blogs.FirstOrDefault(x => x.Id == id);
        }

        public blog Update(blog blog)
        {
            _context.Blogs.Update(blog);
            _context.SaveChanges();
            return blog;
        }
        
        public void Delete(int id)
        {
            var blog = GetById(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }

        
        