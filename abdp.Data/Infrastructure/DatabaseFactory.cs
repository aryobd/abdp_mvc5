using abdp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private OlssEntities _dataContext;

        public OlssEntities Get()
        {
            if (_dataContext == null)
            {
                _dataContext = new OlssEntities();
                _dataContext.Database.CommandTimeout = 180;

                return _dataContext;
            }
            else
            {
                _dataContext.Database.CommandTimeout = 180;

                return _dataContext;
            }
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
