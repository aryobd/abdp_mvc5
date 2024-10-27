using abdp.BLL.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.BLL.IServices
{
    public interface IBllOlssModelVehicle
    {
        int TotalRows();
        int TotalRows(Expression<Func<BllOlssModelVehicleModel, bool>> where);

        List<BllOlssModelVehicleModel> GetList(
            Expression<Func<BllOlssModelVehicleModel, bool>> where,
            int take,
            int skip,
            Expression<Func<BllOlssModelVehicleModel, string>> sort,
            string sortDirection
        );
    }
}
