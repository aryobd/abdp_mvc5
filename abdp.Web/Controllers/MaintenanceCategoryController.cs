using abdp.Service.IServices;
using abdp.Service.Models;
using abdp.Web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace abdp.Web.Controllers
{
    public class MaintenanceCategoryController : Controller
    {
        private readonly IMaintenanceCategoryService _service;

        public MaintenanceCategoryController(IMaintenanceCategoryService maintenanceCategoryService)
        {
            _service = maintenanceCategoryService;
        }

        // GET: MaintenanceCategory
        public ActionResult Index()
        {
            var data1 = _service.SelectById(7);

            var model = new MaintenanceCategoryViewModel
            {
                IdMaintenanceCategory = data1.IdMaintenanceCategory,
                MaintenanceCategoryName = data1.MaintenanceCategoryName,
                Remarks = data1.Remarks,
                CreatedDate = data1.CreatedDate,
                CreatedBy = data1.CreatedBy,
                LastModified = data1.LastModified,
                LastModifiedBy = data1.LastModifiedBy,
                IsDraft = data1.IsDraft,
                IsSubmitted = data1.IsSubmitted,
                IsActive = data1.IsActive,
                IsDeleted = data1.IsDeleted
            };

            Expression<Func<MaintenanceCategoryListInfo, bool>> filter = null;
            Expression<Func<MaintenanceCategoryListInfo, string>> ordering = c => c.MaintenanceCategoryName;

            var data2 = _service.GetList(filter, 0, 0, ordering, "asc");
            List<MaintenanceCategoryListInfo> ItemList = _service.GetList(filter, 0, 0, ordering, "asc").ToList();

            //return View();
            return View(model);
        }
    }
}