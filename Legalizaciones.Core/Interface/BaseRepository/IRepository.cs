using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Legalizaciones.Interface.BaseRepository
{
    public interface IRepository<T> where T : BaseModel
    {
        IEnumerable<T> All();
        T Find(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
