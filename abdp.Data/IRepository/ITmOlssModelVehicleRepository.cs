using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data.IRepository
{
    public interface ITmOlssModelVehicleRepository
    {
        IEnumerable<tm_olss_model_vehicle> SelectAll();
        tm_olss_model_vehicle SelectSingle(Expression<Func<tm_olss_model_vehicle, bool>> where);
        tm_olss_model_vehicle SelectById(int id);

        IQueryable<tm_olss_model_vehicle> AsQueryable(Expression<Func<tm_olss_model_vehicle, bool>> where);
        IQueryable<tm_olss_model_vehicle> AsQueryable();

        void Insert(tm_olss_model_vehicle obj);
        void Update(tm_olss_model_vehicle obj);
    }
}
