using olss.Data.Entities;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace olss.Data.Infrastructure
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        private OlssEntities _db = null;
        protected readonly IDbSet<TEntity> dbset;

        public RepositoryBase(IDatabaseFactoryOlss databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
        }

        protected IDatabaseFactoryOlss DatabaseFactory
        {
            get;
            private set;
        }

        protected OlssEntities DataContext
        {
            get { return _db ?? (_db = DatabaseFactory.Get()); }
        }

        public IEnumerable<TEntity> SelectAll()
        {
            return dbset.AsEnumerable();
        }

        public TEntity SelectById(int Id)
        {
            return dbset.Find(Id);
        }

        public TEntity SelectByNullableId(int? Id)
        {
            return dbset.Find(Id);
        }

        public void Insert(TEntity obj)
        {
            dbset.Add(obj);
        }

        public void Update(TEntity obj)
        {
            dbset.Attach(obj);

            _db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(TEntity obj)
        {
            dbset.Remove(obj);
        }

        public void UpdateWithRowVersion(TEntity obj, byte[] rowVersion)
        {
            dbset.Attach(obj);

            _db.Entry(obj).OriginalValues["RowVersion"] = rowVersion;
            _db.Entry(obj).State = EntityState.Modified;
        }

        public IQueryable<TEntity> AsQueryable(System.Linq.Expressions.Expression<Func<TEntity, bool>> where)
        {
            return dbset.AsQueryable().Where(where);
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dbset.AsQueryable();
        }
    }
}
