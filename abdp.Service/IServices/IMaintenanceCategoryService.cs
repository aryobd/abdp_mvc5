using abdp.Data.Entities;
using abdp.Service.Models;

//using Dsf.Olss.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.IServices
{
    public interface IMaintenanceCategoryService
    {
        bool DataCreate(MaintenanceCategory maintenanceCategory);
        bool DataEdit(MaintenanceCategory maintenanceCategory);
        MaintenanceCategory SelectById(int id);
        int TotalRows();
        int TotalRows(Expression<Func<MaintenanceCategoryListInfo, bool>> where);
        IEnumerable<MaintenanceCategoryListInfo> GetList(
            Expression<Func<MaintenanceCategoryListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryListInfo, string>> sort,
            string sortDirection
        );
        int TotalActiveRows();
        int TotalActiveMaintenanceCategory(Expression<Func<MaintenanceCategoryListInfo, bool>> where);
        IEnumerable<MaintenanceCategoryListInfo> GetActiveList(
            Expression<Func<MaintenanceCategoryListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryListInfo, string>> sort,
            string sortDirection
        );
        bool IsExistCategoryName(string name);
        bool IsExistCategoryNameExceptMe(int id, string name);
    }
}
