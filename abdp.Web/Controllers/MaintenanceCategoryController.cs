using abdp.Service.IServices;
using abdp.Service.Models;
using abdp.Web.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace abdp.Web.Controllers
{
    public class MaintenanceCategoryController : Controller
    {
        private readonly IMaintenanceCategoryService _service;

        public MaintenanceCategoryController(IMaintenanceCategoryService service)
        {
            _service = service;
        }   

        // GET: MaintenanceCategory
        public ActionResult Index()
        {
            /*
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

            Expression<Func<MaintenanceCategoryServiceModel, bool>> filter = null;
            Expression<Func<MaintenanceCategoryServiceModel, string>> ordering = c => c.MaintenanceCategoryName;

            var data2 = _service.GetList(filter, 0, 0, ordering, "asc");
            List<MaintenanceCategoryServiceModel> ItemList = _service.GetList(filter, 0, 0, ordering, "asc").ToList();
            */

            return View();
            //return View(model);
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            try
            {
                #region Set Filter
                Expression<Func<MaintenanceCategoryServiceModel, bool>> filter = null;

                if (param.sSearch != null)
                    filter = (
                        o => o.MaintenanceCategoryName.Contains(param.sSearch)
                        ||
                        o.Remarks.Contains(param.sSearch)
                        ||
                        (
                            SqlFunctions.StringConvert((double)o.CreatedDate.Month).TrimStart() + "/" +
                            SqlFunctions.DateName("day", o.CreatedDate).Trim() + "/" +
                            SqlFunctions.DateName("year", o.CreatedDate)
                        ).Contains(param.sSearch)
                    );
                #endregion Set Filter

                #region Set Sorting & Ordering
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<MaintenanceCategoryServiceModel, string>> ordering = (
                    o => sortColumnIndex == 0 ? o.MaintenanceCategoryName :
                         sortColumnIndex == 1 ? o.Remarks :
                         SqlFunctions.StringConvert((double)SqlFunctions.DateDiff("day", DateTime.MinValue, o.CreatedDate))
                );
                var sortDirection = Request["sSortDir_0"]; // ASC OR DESC
                #endregion Set Sorting & Ordering

                var listData = _service.GetList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);
                var result = from o in listData
                             select new
                             {
                                 o.IdMaintenanceCategory,
                                 o.MaintenanceCategoryName,
                                 o.Remarks,
                                 o.IsActive,
                                 o.IsSubmitted,
                                 o.IsDraft,
                                 o.IsDeleted,
                                 o.CreatedBy,
                                 o.CreatedDate,
                                 o.LastModifiedBy,
                                 o.LastModified
                             };

                return Json(new
                    {
                        param.sEcho,
                        iTotalRecords = 0,
                        iTotalDisplayRecords = 0,
                        aaData = result.ToList()
                    },
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}