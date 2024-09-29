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
    public class MaintenanceItemService : IMaintenanceItemService
    {

        private readonly IMaintenanceItemRepository _repository;
        //private readonly IGenericRepository<OptionItem> _optionItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MaintenanceItemService(
            IMaintenanceItemRepository repository,
            //IGenericRepository<OptionItem> optionItemRepository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            //_optionItemRepository = optionItemRepository;
            _unitOfWork = unitOfWork;
        }

        private void DataSave()
        {
            _unitOfWork.Save();
        }

        public bool DataCreate(MaintenanceItem maintenanceItem)
        {
            if (maintenanceItem == null)
                throw new ArgumentNullException("maintenanceItem");

            try
            {
                _repository.Insert(maintenanceItem);

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

        public bool DataEdit(MaintenanceItem maintenanceItem)
        {
            if (maintenanceItem == null)
                throw new ArgumentNullException("maintenanceItem");

            if (maintenanceItem.IdMaintenanceItem == 0)
                throw new ArgumentException("0 is invalid MaintenanceItem id", "maintenanceItem");

            try
            {
                _repository.Update(maintenanceItem);

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

        public MaintenanceItem SelectById(int id)
        {
            if (id == 0)
                throw new ArgumentException("id should not be 0", "id");

            var data = _repository.SelectById(id);

            if (data.IsDeleted)
                return null;
            else
                return data;
        }

        private IQueryable<MaintenanceItemListInfo> Query
        {
            get
            {
                return (from repo in _repository.AsQueryable()
                        where repo.IsDeleted.Equals(false)
                        select new MaintenanceItemListInfo()
                        {
                            IdMaintenanceItem = repo.IdMaintenanceItem,
                            MaintenanceItemName = repo.MaintenanceItemName,
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

        public int TotalRows(Expression<Func<MaintenanceItemListInfo, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<MaintenanceItemListInfo> GetList(Expression<Func<MaintenanceItemListInfo, bool>> where, int take, int skip, Expression<Func<MaintenanceItemListInfo, string>> sort, string sortDirection)
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

        public int TotalActiveRows(Expression<Func<MaintenanceItemListInfo, bool>> where)
        {
            if (where == null)
                return TotalActiveRows();

            return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).Count();
        }

        public IEnumerable<MaintenanceItemListInfo> GetActiveList(Expression<Func<MaintenanceItemListInfo, bool>> where, int take, int skip, Expression<Func<MaintenanceItemListInfo, string>> sort, string sortDirection)
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

        public bool IsExistItemName(string name)
        {
            try
            {
                var data = _repository.SelectByName(name);

                if (data == null)
                    return false;

                return data.IdMaintenanceItem > 0;
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

        public bool IsExistItemNameExceptMe(int id, string name)
        {
            try
            {
                Expression<Func<MaintenanceItem, bool>> filter = (
                    c => c.MaintenanceItemName.Equals(name)
                    &&
                    !c.IdMaintenanceItem.Equals(id)
                );

                var data = _repository.SelectSingle(filter);

                if (data == null)
                    return false;

                return data.IdMaintenanceItem > 0;
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

        /*
        public IEnumerable<OptionItemValue> GetReplacementCycleUnit()
        {
            var optionIntem = _optionItemRepository.AsQueryable().FirstOrDefault(o => o.OptionItemName.Equals("Replacement Cycle Unit"));
            return optionIntem != null ? optionIntem.OptionItemValues.AsEnumerable() : null;
        }
        */
    }
}
