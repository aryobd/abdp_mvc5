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
    public class TmOlssBrandService : ITmOlssBrandService
    {
        private readonly ITmOlssBrandRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TmOlssBrandService(ITmOlssBrandRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private IQueryable<TmOlssBrandServiceModel> Query
        {
            get
            {
                return (
                    from repo in _repository.AsQueryable()
                    select new TmOlssBrandServiceModel()
                    {
                        tm_olss_brand_id = repo.tm_olss_brand_id,
                        tm_olss_brand_id_prev = repo.tm_olss_brand_id_prev,
                        brand_name = repo.brand_name,
                        brand_desc = repo.brand_desc
                    }
                );
            }
        }

        private void DataSave()
        {
            _unitOfWork.Save();
        }

        public bool DataCreate(tm_olss_brand tmOlssBrand)
        {
            if (tmOlssBrand == null)
                throw new ArgumentNullException("tm_olss_brand");

            try
            {
                _repository.Insert(tmOlssBrand);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DataEdit(tm_olss_brand tmOlssBrand)
        {
            if (tmOlssBrand == null)
                throw new ArgumentNullException("tm_olss_brand");

            if (tmOlssBrand.tm_olss_brand_id == 0)
                throw new ArgumentException("0 is invalid tm_olss_brand.tm_olss_brand_id", "tm_olss_brand");

            try
            {
                _repository.Update(tmOlssBrand);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public tm_olss_brand SelectById(int id)
        {
            if (id == 0)
                throw new ArgumentException("id should not be 0", "id");

            return _repository.SelectById(id);
        }

        public int TotalRows()
        {
            return Query.Count();
        }

        public int TotalRows(Expression<Func<TmOlssBrandServiceModel, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<TmOlssBrandServiceModel> GetList(
            Expression<Func<TmOlssBrandServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<TmOlssBrandServiceModel, string>> sort,
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
