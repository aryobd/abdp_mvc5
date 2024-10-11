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
    public class TmOlssBrandRepository : RepositoryBase<tm_olss_brand>, ITmOlssBrandRepository
    {
        public TmOlssBrandRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public tm_olss_brand SelectSingle(Expression<Func<tm_olss_brand, bool>> where)
        {
            return dbset.Where(where).SingleOrDefault();
        }
    }
}
