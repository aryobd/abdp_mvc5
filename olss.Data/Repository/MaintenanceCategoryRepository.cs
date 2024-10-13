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
    public class MaintenanceCategoryRepository : RepositoryBase<MaintenanceCategory>, IMaintenanceCategoryRepository
    {
        public MaintenanceCategoryRepository(IDatabaseFactoryOlss databaseFactory)
            : base(databaseFactory) 
        {
        }

        public MaintenanceCategory SelectSingle(Expression<Func<MaintenanceCategory, bool>> where)
        {
            return dbset.Where(where).SingleOrDefault();
        }

        public MaintenanceCategory SelectByName(string name)
        {
            return dbset.Where(o => o.MaintenanceCategoryName.Equals(name)).FirstOrDefault();
        }
    }
}
