
using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;
using blogWithAPI.Entity.Results;

namespace blogWithAPI.BusinessLayer.Abstract
{
    public interface IBlogService
    {
        IDataResult<List<blog>> GetAll();
        IDataResult<blog> GetById(int id);
        IResult Add(blog blog);
        IResult Update(blog blog);
        IResult Delete(int id);
    }

}
