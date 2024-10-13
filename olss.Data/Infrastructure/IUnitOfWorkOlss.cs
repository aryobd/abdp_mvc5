using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data.Infrastructure
{
    public interface IUnitOfWorkOlss
    {
        void Save();
        Task<bool> SaveAsync();
        void ExecuteSqlCommand(string strsql);
        void ExecuteSqlCommand(string strsql, SqlParameter[] param);
    }
}
