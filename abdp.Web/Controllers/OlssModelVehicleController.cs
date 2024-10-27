using abdp.BLL.IServices;
using abdp.BLL.Models;

using abdp.Service;
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
    public class OlssModelVehicleController : Controller
    {
        private readonly ITmOlssModelVehicleService _service;
        private readonly IBllOlssModelVehicle _bllService;

        public OlssModelVehicleController(
            ITmOlssModelVehicleService service,
            IBllOlssModelVehicle bllService
        )
        {
            _service = service;
            _bllService = bllService;
        }

        // GET: OlssModelVehicle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            try
            {
                #region SET FILTER
                Expression<Func<TmOlssModelVehicleServiceModel, bool>> filter = null;
                Expression<Func<BllOlssModelVehicleModel, bool>> bllFilter = null;

                if (param.sSearch != null)
                {
                    filter = (
                        o => o.model_vehicle_name.Contains(param.sSearch)
                             ||
                             o.model_vehicle_desc.Contains(param.sSearch)
                             ||
                             o.brand_name.Contains(param.sSearch)
                    );

                    bllFilter = (
                        o => o.model_vehicle_name.Contains(param.sSearch)
                             ||
                             o.model_vehicle_desc.Contains(param.sSearch)
                             ||
                             o.brand_name.Contains(param.sSearch)
                    );
                }
                #endregion SET FILTER

                #region SET SORTING & ORDERING
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<TmOlssModelVehicleServiceModel, string>> ordering = (
                    o => sortColumnIndex == 0 ? o.brand_name :
                         sortColumnIndex == 1 ? o.model_vehicle_name :
                         o.model_vehicle_desc
                );
                Expression<Func<BllOlssModelVehicleModel, string>> bllOrdering = (
                    o => sortColumnIndex == 0 ? o.brand_name :
                         sortColumnIndex == 1 ? o.model_vehicle_name :
                         o.model_vehicle_desc
                );
                var sortDirection = Request["sSortDir_0"]; // ASC / DESC
                #endregion SET SORTING & ORDERING

                var listData = _service.GetList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);
                var result = from o in listData
                             select new
                             {
                                 o.tm_olss_model_vehicle_id,
                                 o.tm_olss_model_vehicle_id_prev,
                                 o.tm_olss_brand_id,
                                 o.model_vehicle_name,
                                 o.model_vehicle_desc,
                                 o.brand_name
                             };

                var bllListData = _bllService.GetList(bllFilter, param.iDisplayLength, param.iDisplayStart, bllOrdering, sortDirection);
                var bllResult = from o in bllListData
                                select new
                                {
                                    o.tm_olss_model_vehicle_id,
                                    o.tm_olss_model_vehicle_id_prev,
                                    o.tm_olss_brand_id,
                                    o.model_vehicle_name,
                                    o.model_vehicle_desc,
                                    o.brand_name
                                };

                return Json(new
                {
                    param.sEcho,

                    //iTotalRecords = _service.TotalRows(),
                    //iTotalDisplayRecords = _service.TotalRows(filter),
                    //aaData = result.ToList()

                    iTotalRecords = _bllService.TotalRows(),
                    iTotalDisplayRecords = _bllService.TotalRows(bllFilter),
                    aaData = bllResult.ToList()
                },
                    JsonRequestBehavior.AllowGet
                );
            }
            catch (Exception ex)
            {
                return View("Error" + "/n" + ex.Message);
            }
        }
    }
}
