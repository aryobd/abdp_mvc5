using olss.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data.IRepository
{
    public interface IMaintenanceCategoryRepository
    {
        IEnumerable<MaintenanceCategory> SelectAll();
        MaintenanceCategory SelectSingle(Expression<Func<MaintenanceCategory, bool>> where);
        MaintenanceCategory SelectById(int id);
        MaintenanceCategory SelectByName(string name);

        IQueryable<MaintenanceCategory> AsQueryable(Expression<Func<MaintenanceCategory, bool>> where);
        IQueryable<MaintenanceCategory> AsQueryable();

        void Insert(MaintenanceCategory obj);
        void Update(MaintenanceCategory obj);
    }
}
