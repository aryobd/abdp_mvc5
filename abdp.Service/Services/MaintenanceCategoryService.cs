using abdp.Data;
using abdp.Data.Entities;
using abdp.Data.Infrastructure;
using abdp.Data.IRepository;

using abdp.Service.IServices;
using abdp.Service.Models;

//using Dsf.Olss.Data;
//using Dsf.Olss.Data.Entities;
//using Dsf.Olss.Data.Infrastructure;

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
        private readonly IUnitOfWork _unitOfWork;

        public MaintenanceCategoryService(IMaintenanceCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
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
            /*
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format(
                            "{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage
                        );
                        
                        // raise a new exception nesting the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                //throw raise;
                Tracer.Error(raise.ToString());

                return false;
            }
            */
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
            /*
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format(
                            "{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage
                        );
                        
                        // raise a new exception nesting the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                //throw raise;
                Tracer.Error(raise.ToString());

                return false;
            }
            */
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

        private IQueryable<MaintenanceCategoryListInfo> Query
        {
            get
            {
                return (from repo in _repository.AsQueryable()
                        where repo.IsDeleted.Equals(false)
                        select new MaintenanceCategoryListInfo()
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
                        });
            }
        }

        public int TotalRows()
        {
            return Query.Count();
        }

        public int TotalRows(Expression<Func<MaintenanceCategoryListInfo, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<MaintenanceCategoryListInfo> GetList(
            Expression<Func<MaintenanceCategoryListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryListInfo, string>> sort,
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

        public int TotalActiveMaintenanceCategory(Expression<Func<MaintenanceCategoryListInfo, bool>> where)
        {
            if (where == null)
                return TotalActiveRows();

            return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).Count();
        }

        public IEnumerable<MaintenanceCategoryListInfo> GetActiveList(
            Expression<Func<MaintenanceCategoryListInfo, bool>> where,
            int take,
            int skip,
            Expression<Func<MaintenanceCategoryListInfo, string>> sort,
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
            /*
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format(
                            "{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage
                        );
                        
                        // raise a new exception nesting the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                //throw raise;
                Tracer.Error(raise.ToString());

                return false;
            }
            */
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
            /*
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format(
                            "{0}:{1}",
                            validationErrors.Entry.Entity,
                            validationError.ErrorMessage
                        );
                        
                        // raise a new exception nesting the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }

                //throw raise;
                Tracer.Error(raise.ToString());

                return false;
            }
            */
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
