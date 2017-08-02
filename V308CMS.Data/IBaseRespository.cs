using System.Collections.Generic;

namespace V308CMS.Data
{
    public interface IBaseRespository<T>
    {
        T Find(int id);
        string Delete(int id);
        string Update(T t);
        string Insert(T t);
        List<T> GetList(int page = 1, int pageSize = 10,string site="",byte status=0);
    }
}
