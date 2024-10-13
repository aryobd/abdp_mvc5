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
    public class MaintenanceItemService : IMaintenanceItemService
    {

        private readonly IMaintenanceItemRepository _repository;
        //private readonly IGenericRepository<OptionItem> _optionItemRepository;
        private readonly IUnitOfWorkOlss _unitOfWork;

        public MaintenanceItemService(
            IMaintenanceItemRepository repository,
            //IGenericRepository<OptionItem> optionItemRepository,
            IUnitOfWorkOlss unitOfWork
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

        private IQueryable<MaintenanceItemServiceModel> Query
        {
            get
            {
                return (from repo in _repository.AsQueryable()
                        where repo.IsDeleted.Equals(false)
                        select new MaintenanceItemServiceModel()
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

        public int TotalRows(Expression<Func<MaintenanceItemServiceModel, bool>> where)
        {
            if (where == null)
                return TotalRows();

            return Query.Where(where).Count();
        }

        public IEnumerable<MaintenanceItemServiceModel> GetList(Expression<Func<MaintenanceItemServiceModel, bool>> where, int take, int skip, Expression<Func<MaintenanceItemServiceModel, string>> sort, string sortDirection)
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

        public int TotalActiveRows(Expression<Func<MaintenanceItemServiceModel, bool>> where)
        {
            if (where == null)
                return TotalActiveRows();

            return Query.Where(where).Where(o => o.IsActive && !o.IsDraft).Count();
        }

        public IEnumerable<MaintenanceItemServiceModel> GetActiveList(Expression<Func<MaintenanceItemServiceModel, bool>> where, int take, int skip, Expression<Func<MaintenanceItemServiceModel, string>> sort, string sortDirection)
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
