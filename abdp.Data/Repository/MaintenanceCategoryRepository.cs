using abdp.Data.Entities;
using abdp.Data.Infrastructure;
using abdp.Data.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.Repository
{
    public class MaintenanceCategoryRepository : RepositoryBase<MaintenanceCategory>, IMaintenanceCategoryRepository
    {
        public MaintenanceCategoryRepository(IDatabaseFactory databaseFactory)
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
