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
    public interface ITmOlssModelVehicleService
    {
        bool DataCreate(tm_olss_model_vehicle tmOlssModelVehicle);
        bool DataEdit(tm_olss_model_vehicle tmOlssBrand);

        tm_olss_model_vehicle SelectById(int id);

        int TotalRows();
        int TotalRows(Expression<Func<TmOlssModelVehicleServiceModel, bool>> where);

        IEnumerable<TmOlssModelVehicleServiceModel> GetList(
            Expression<Func<TmOlssModelVehicleServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<TmOlssModelVehicleServiceModel, string>> sort,
            string sortDirection
        );
    }
}
