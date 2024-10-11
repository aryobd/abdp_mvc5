using abdp.Data.Entities;
using abdp.Service.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.IServices
{
    public interface ITmOlssBrandService
    {
        bool DataCreate(tm_olss_brand tmOlssBrand);
        bool DataEdit(tm_olss_brand tmOlssBrand);

        tm_olss_brand SelectById(int id);

        int TotalRows();
        int TotalRows(Expression<Func<TmOlssBrandServiceModel, bool>> where);

        IEnumerable<TmOlssBrandServiceModel> GetList(
            Expression<Func<TmOlssBrandServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<TmOlssBrandServiceModel, string>> sort,
            string sortDirection
        );
    }
}
