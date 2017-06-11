using System.Collections.Generic;

namespace V308CMS.Respository
{
    public interface IBaseRespository<T>
    {
        T Find(int id);
        string Delete(int id);
        string Update(T t);
        string Insert(T t);
        List<T> GetAll();
    }
}
