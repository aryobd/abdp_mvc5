using abdp.Data.Entities;
using abdp.Data.Infrastructure;
using abdp.Data.IRepository;

using abdp.Service.IServices;
using abdp.Service.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.Services
{
    public class TmOlssModelVehicleService : ITmOlssModelVehicleService
    {
        private readonly ITmOlssModelVehicleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TmOlssModelVehicleService(ITmOlssModelVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private IQueryable<TmOlssModelVehicleServiceModel> Query
        {
            get
            {
                return (
                    from repo in _repository.AsQueryable()
                    select new TmOlssModelVehicleServiceModel()
                    {
                        tm_olss_model_vehicle_id = repo.tm_olss_model_vehicle_id,
                        tm_olss_model_vehicle_id_prev = repo.tm_olss_model_vehicle_id_prev,
                        tm_olss_brand_id = repo.tm_olss_brand_id,
                        model_vehicle_name = repo.model_vehicle_name,
                        model_vehicle_desc = repo.model_vehicle_desc
                    }
                );
            }
        }

        private void DataSave()
        {
            _unitOfWork.Save();
        }

        public bool DataCreate(tm_olss_model_vehicle TmOlssModelVehicle)
        {
            if (TmOlssModelVehicle == null)
                throw new ArgumentNullException("tm_olss_model_vehicle");

            try
            {
                _repository.Insert(TmOlssModelVehicle);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DataEdit(tm_olss_model_vehicle TmOlssModelVehicle)
        {
            if (TmOlssModelVehicle == null)
                throw new ArgumentNullException("tm_olss_model_vehicle");

            if (TmOlssModelVehicle.tm_olss_model_vehicle_id == 0)
                throw new ArgumentException("0 is invalid tm_olss_model_vehicle.tm_olss_model_vehicle_id", "tm_olss_model_vehicle");

            try
            {
                _repository.Update(TmOlssModelVehicle);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public tm_olss_model_vehicle SelectById(int id)
        {
            if (id == 0)
                throw new ArgumentException("id should not be 0", "id");

            return _repository.SelectById(id);
        }

        public int TotalRows()
        {
            return Query.Count();
        }

        public int TotalRows(Expression<Func<TmOlssModelVehicleServiceModel, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<TmOlssModelVehicleServiceModel> GetList(
            Expression<Func<TmOlssModelVehicleServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<TmOlssModelVehicleServiceModel, string>> sort,
            string sortDirection
        )
        {
            if (sortDirection.Equals("asc"))
            {
                if (where == null)
                    return Query.OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
                else
                    return Query.Where(where).OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
            }
            else
            {
                if (where == null)
                    return Query.OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
                else
                    return Query.Where(where).OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
            }
        }
    }
}
