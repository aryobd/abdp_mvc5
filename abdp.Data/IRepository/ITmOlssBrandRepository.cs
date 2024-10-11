using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.IRepository
{
    public interface ITmOlssBrandRepository
    {
        IEnumerable<tm_olss_brand> SelectAll();
        tm_olss_brand SelectSingle(Expression<Func<tm_olss_brand, bool>> where);
        tm_olss_brand SelectById(int id);

        IQueryable<tm_olss_brand> AsQueryable(Expression<Func<tm_olss_brand, bool>> where);
        IQueryable<tm_olss_brand> AsQueryable();

        void Insert(tm_olss_brand obj);
        void Update(tm_olss_brand obj);
    }
}
