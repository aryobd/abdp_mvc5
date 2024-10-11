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
    public class TmOlssBrandController : Controller
    {
        private readonly ITmOlssBrandService _service;

        public TmOlssBrandController(ITmOlssBrandService service)
        {
            _service = service;
        }

        public ActionResult AjaxHandler(JQueryDataTableParamModel param)
        {
            try
            {
                #region SET FILTER
                Expression<Func<TmOlssBrandServiceModel, bool>> filter = null;

                if (param != null)
                {
                    filter = (
                        o => o.brand_name.Contains(param.sSearch)
                             ||
                             o.brand_desc.Contains(param.sSearch)
                    );
                }
                #endregion SET FILTER

                #region SET SORTING & ORDERING
                var sortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
                Expression<Func<TmOlssBrandServiceModel, string>> ordering = (
                    o => sortColumnIndex == 0 ? o.brand_name : o.brand_desc
                );
                var sortDirection = Request["sSortDir_0"]; // ASC / DESC
                #endregion SET SORTING & ORDERING

                var listData = _service.GetList(filter, param.iDisplayLength, param.iDisplayStart, ordering, sortDirection);
                var result = from o in listData
                             select new
                             {
                                 o.tm_olss_brand_id,
                                 o.tm_olss_brand_id_prev,
                                 o.brand_name,
                                 o.brand_desc
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

        // GET: TmOlssBrand
        public ActionResult Index()
        {
            var data = _service.SelectById(1007);

            return View();
        }
    }
}