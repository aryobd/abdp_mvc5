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
    public interface IMaintenanceCategoryService
    {
        bool DataCreate(MaintenanceCategory maintenanceCategory);
        bool DataEdit(MaintenanceCategory maintenanceCategory);

        MaintenanceCategory SelectById(int id);

        int TotalRows();
        int TotalRows(Expression<Func<MaintenanceCategoryServiceModel, bool>> where);

        IEnumerable<MaintenanceCategoryServiceModel> GetList(
            Expression<Func<MaintenanceCategoryServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryServiceModel, string>> sort,
            string sortDirection
        );

        int TotalActiveRows();
        int TotalActiveMaintenanceCategory(Expression<Func<MaintenanceCategoryServiceModel, bool>> where);

        IEnumerable<MaintenanceCategoryServiceModel> GetActiveList(
            Expression<Func<MaintenanceCategoryServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryServiceModel, string>> sort,
            string sortDirection
        );

        bool IsExistCategoryName(string name);
        bool IsExistCategoryNameExceptMe(int id, string name);
    }
}
