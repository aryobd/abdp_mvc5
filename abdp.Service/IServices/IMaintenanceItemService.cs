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
    public interface IMaintenanceItemService
    {
        bool DataCreate(MaintenanceItem maintenanceItem);
        bool DataEdit(MaintenanceItem maintenanceItem);
        MaintenanceItem SelectById(int id);
        int TotalRows();
        int TotalRows(Expression<Func<MaintenanceItemServiceModel, bool>> where);
        IEnumerable<MaintenanceItemServiceModel> GetList(
            Expression<Func<MaintenanceItemServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceItemServiceModel, string>> sort,
            string sortDirection
        );
        int TotalActiveRows();
        int TotalActiveRows(Expression<Func<MaintenanceItemServiceModel, bool>> where);
        IEnumerable<MaintenanceItemServiceModel> GetActiveList(
            Expression<Func<MaintenanceItemServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceItemServiceModel, string>> sort,
            string sortDirection
        );
        bool IsExistItemName(string name);
        bool IsExistItemNameExceptMe(int id, string name);
        //IEnumerable<OptionItemValue> GetReplacementCycleUnit();
    }
}
