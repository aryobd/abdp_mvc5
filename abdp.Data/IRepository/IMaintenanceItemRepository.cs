using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.IRepository
{
    public interface IMaintenanceItemRepository
    {
        IEnumerable<MaintenanceItem> SelectAll();
        MaintenanceItem SelectSingle(Expression<Func<MaintenanceItem, bool>> where);
        MaintenanceItem SelectById(int id);
        MaintenanceItem SelectByName(string name);

        IQueryable<MaintenanceItem> AsQueryable(Expression<Func<MaintenanceItem, bool>> where);
        IQueryable<MaintenanceItem> AsQueryable();

        void Insert(MaintenanceItem obj);
        void Update(MaintenanceItem obj);
    }
}
