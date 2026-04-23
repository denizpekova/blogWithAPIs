
using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;

namespace blogWithAPI.DataAccessLayer.Abstract
{
    public interface IBlogRepository
    {
        List<Blog> GetAll();
        Blog? GetById(int id);
        Blog Add(Blog blog);
        Blog Update(Blog blog);
        void Delete(int id);
    }

}
