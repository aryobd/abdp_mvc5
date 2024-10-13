using olss.Data.Entities;
using olss.Data.Infrastructure;
using olss.Data.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data.Repository
{
    public class MaintenanceItemRepository : RepositoryBase<MaintenanceItem>, IMaintenanceItemRepository
    {
        public MaintenanceItemRepository(IDatabaseFactoryOlss databaseFactory)
            : base(databaseFactory)
        {
        }

        public MaintenanceItem SelectSingle(Expression<Func<MaintenanceItem, bool>> where)
        {
            return dbset.Where(where).SingleOrDefault();
        }

        //public MaintenanceItem SelectById(int id)
        //{
        //    return dbset.Where(o => o.IdMaintenanceItem.Equals(id)).FirstOrDefault();
        //}

        public MaintenanceItem SelectByName(string name)
        {
            return dbset.Where(o => o.MaintenanceItemName.Equals(name)).FirstOrDefault();
        }
    }
}
