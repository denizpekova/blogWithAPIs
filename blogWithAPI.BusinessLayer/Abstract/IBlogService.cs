
using blogWithAPI.Entity.Concrete;
using System.Collections.Generic;
using blogWithAPI.Entity.Results;

namespace blogWithAPI.BusinessLayer.Abstract
{
    public interface IBlogService
    {
        IDataResult<List<Blog>> GetAll();
        IDataResult<Blog> GetById(int id);
        IResult Add(Blog blog);
        IResult Update(Blog blog);
        IResult Delete(int id);
    }

}
