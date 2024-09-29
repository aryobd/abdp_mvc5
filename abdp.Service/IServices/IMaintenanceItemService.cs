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
    public interface IMaintenanceItemService
    {
        bool DataCreate(MaintenanceItem maintenanceItem);
        bool DataEdit(MaintenanceItem maintenanceItem);
        MaintenanceItem SelectById(int id);
        int TotalRows();
        int TotalRows(Expression<Func<MaintenanceItemListInfo, bool>> where);
        IEnumerable<MaintenanceItemListInfo> GetList(
            Expression<Func<MaintenanceItemListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceItemListInfo, string>> sort,
            string sortDirection
        );
        int TotalActiveRows();
        int TotalActiveRows(Expression<Func<MaintenanceItemListInfo, bool>> where);
        IEnumerable<MaintenanceItemListInfo> GetActiveList(
            Expression<Func<MaintenanceItemListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceItemListInfo, string>> sort,
            string sortDirection
        );
        bool IsExistItemName(string name);
        bool IsExistItemNameExceptMe(int id, string name);
        //IEnumerable<OptionItemValue> GetReplacementCycleUnit();
    }
}
