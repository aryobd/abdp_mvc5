using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Save();
        Task<bool> SaveAsync();
        void ExecuteSqlCommand(string strsql);
        void ExecuteSqlCommand(string strsql, SqlParameter[] param);
    }
}
