
using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;

namespace blogWithAPI.BusinessLayer.Abstract
{
    public interface IBlogService
    {
        List<blog> GetAll();
        blog GetById(int id);
        blog Add(blog blog);
        blog Update(blog blog);
        void Delete(int id);
    }

}
