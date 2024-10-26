using abdp.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Data
{
    public interface ITmOlssModelVehicleRepository
    {
        IEnumerable<tm_olss_model_vehicle> SelectAll();
        tm_olss_model_vehicle SelectById(int id);

        IQueryable<tm_olss_model_vehicle> AsQueryable();
        IQueryable<tm_olss_model_vehicle> AsQueryable(Expression<Func<tm_olss_model_vehicle, bool>> where);

        void Insert(tm_olss_model_vehicle obj);
        void Update(tm_olss_model_vehicle obj);
    }
}
