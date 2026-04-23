using blogWithAPI.DataAccessLayer.Abstract;
using blogWithAPI.DataAccessLayer.Concrete;
using blogWithAPI.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace blogWithAPI.DataAccessLayer.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        private readonly Context _context;

        public BlogRepository(Context context)
        {
            _context = context;
        }

        public Blog Add(Blog blog)
        {
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return blog;
        }

        public List<Blog> GetAll()
        {
            return _context.Blogs.ToList();
        }

        public Blog? GetById(int id)
        {
            return _context.Blogs.FirstOrDefault(x => x.Id == id);
        }

        public Blog Update(Blog blog)
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
    }
}