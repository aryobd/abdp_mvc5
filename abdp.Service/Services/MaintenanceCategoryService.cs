using olss.Data.Entities;
using olss.Data.Infrastructure;
using olss.Data.IRepository;

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
    public class MaintenanceCategoryService : IMaintenanceCategoryService
    {
        private readonly IMaintenanceCategoryRepository _repository;
        private readonly IUnitOfWorkOlss _unitOfWork;

        public MaintenanceCategoryService(IMaintenanceCategoryRepository repository, IUnitOfWorkOlss unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        private IQueryable<MaintenanceCategoryServiceModel> Query
        {
            get
            {
                return (
                    from repo in _repository.AsQueryable()
                    where repo.IsDeleted.Equals(false)
                    select new MaintenanceCategoryServiceModel()
                    {
                        IdMaintenanceCategory = repo.IdMaintenanceCategory,
                        MaintenanceCategoryName = repo.MaintenanceCategoryName,
                        Remarks = repo.Remarks,
                        CreatedDate = repo.CreatedDate,
                        CreatedBy = repo.CreatedBy,
                        LastModified = repo.LastModified,
                        LastModifiedBy = repo.LastModifiedBy,
                        IsDraft = repo.IsDraft,
                        IsSubmitted = repo.IsSubmitted,
                        IsActive = repo.IsActive,
                        IsDeleted = repo.IsDeleted
                    }
                );
            }
        }

        private void DataSave()
        {
            _unitOfWork.Save();
        }

        public bool DataCreate(MaintenanceCategory maintenanceCategory)
        {
            if (maintenanceCategory == null)
                throw new ArgumentNullException("maintenanceCategory");

            try
            {
                _repository.Insert(maintenanceCategory);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DataEdit(MaintenanceCategory maintenanceCategory)
        {
            if (maintenanceCategory == null)
                throw new ArgumentNullException("maintenanceCategory");

            if (maintenanceCategory.IdMaintenanceCategory == 0)
                throw new ArgumentException("0 is invalid MaintenanceCategory id", "maintenanceCategory");

            try
            {
                _repository.Update(maintenanceCategory);

                DataSave();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public MaintenanceCategory SelectById(int id)
        {
            if (id == 0)
                throw new ArgumentException("id should not be 0", "id");

            var data = _repository.SelectById(id);

            if (data.IsDeleted)
                return null;
            else
                return data;
        }

        public int TotalRows()
        {
            return Query.Count();
        }

        public int TotalRows(Expression<Func<MaintenanceCategoryServiceModel, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<MaintenanceCategoryServiceModel> GetList(
            Expression<Func<MaintenanceCategoryServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryServiceModel, string>> sort,
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

        public int TotalActiveRows()
        {
            return Query.Where(o => o.IsActive && !o.IsDraft).Count();
        }

        public int TotalActiveMaintenanceCategory(Expression<Func<MaintenanceCategoryServiceModel, bool>> where)
        {
            if (where == null)
                return TotalActiveRows();

            return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).Count();
        }

        public IEnumerable<MaintenanceCategoryServiceModel> GetActiveList(
            Expression<Func<MaintenanceCategoryServiceModel, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryServiceModel, string>> sort,
            string sortDirection
        )
        {
            if (sortDirection.Equals("asc"))
            {
                if (where == null)
                    return Query.Where(o => o.IsActive && !o.IsDraft).OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
                else
                    return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).OrderBy(sort).Skip(skip).Take(take).AsEnumerable();
            }
            else
            {
                if (where == null)
                    return Query.Where(o => o.IsActive && !o.IsDraft).OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
                else
                    return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).OrderByDescending(sort).Skip(skip).Take(take).AsEnumerable();
            }
        }

        public bool IsExistCategoryName(string name)
        {
            try
            {
                var data = _repository.SelectByName(name);

                if (data == null)
                    return false;

                return data.IdMaintenanceCategory > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsExistCategoryNameExceptMe(int id, string name)
        {
            try
            {
                Expression<Func<MaintenanceCategory, bool>> filter = (
                    c => c.MaintenanceCategoryName.Equals(name)
                    &&
                    !c.IdMaintenanceCategory.Equals(id)
                );
                
                var data = _repository.SelectSingle(filter);

                if (data == null)
                    return false;

                return data.IdMaintenanceCategory > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
