using olss.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data
{
    public interface IMaintenanceCategoryRepository
    {
        IEnumerable<MaintenanceCategory> SelectAll();
        MaintenanceCategory SelectById(int id);
        MaintenanceCategory SelectSingle(Expression<Func<MaintenanceCategory, bool>> where);
        MaintenanceCategory SelectByName(string name);

        IQueryable<MaintenanceCategory> AsQueryable();
        IQueryable<MaintenanceCategory> AsQueryable(Expression<Func<MaintenanceCategory, bool>> where);

        void Insert(MaintenanceCategory obj);
        void Update(MaintenanceCategory obj);
    }
}
