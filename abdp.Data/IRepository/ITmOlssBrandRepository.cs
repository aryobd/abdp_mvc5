using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data
{
    public interface ITmOlssBrandRepository
    {
        IEnumerable<tm_olss_brand> SelectAll();
        tm_olss_brand SelectById(int id);
        //tm_olss_brand SelectSingle(Expression<Func<tm_olss_brand, bool>> where);

        IQueryable<tm_olss_brand> AsQueryable();
        IQueryable<tm_olss_brand> AsQueryable(Expression<Func<tm_olss_brand, bool>> where);

        void Insert(tm_olss_brand obj);
        void Update(tm_olss_brand obj);
    }
}
