
using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;

namespace blogWithAPI.DataAccessLayer.Abstract
{
    public interface IBlogRepository
    {
        List<blog> GetAll();
        blog GetById(int id);
        blog Add(blog blog);
        blog Update(blog blog);
        void Delete(int id);
    }

}
