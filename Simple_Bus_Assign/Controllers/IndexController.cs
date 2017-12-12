using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Simple_Bus_Assign.Class;
using Simple_Bus_Assign.Interfaces;
using Simple_Bus_Assign.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple_Bus_Assign.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditMenu()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, IndexViewModel model)
        {
            if (model != null)
            {
                Assign assign = new Assign();
                assign.DeleteAssigned(assign.ConvertToAssign(model));
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, IndexViewModel model)
        {
            Assign assign = new Assign();
            if (model != null)
            {
                assign.InsertAssigned(assign.ConvertToAssign(model));
            }
            
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Editing_Assigned([DataSourceRequest] DataSourceRequest request, IndexViewModel model)
        {
            if (model != null)
            {
                Assign assign = new Assign();
                assign.UpdateAssigned(assign.ConvertToAssign(model));
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult Assigned_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetAssigned().ToDataSourceResult(request));
        }
        private void UpdateAssigned(IndexViewModel model)
        {
            Assign assign = new Assign();
            assign = assign.ConvertToAssign(model);
            assign.UpdateAssigned(assign);
        }
        public static IEnumerable<Route> GetRoutes()
        {
            var assign = new Assign();
            return assign.GetRoutes().Select(route => new Route
            {
                Route_Id = route.Route_Id,
                Route_Name = route.Route_Name,
                City = route.City,
                State = route.State,
                Zip = route.Zip
            });
        }
        public ActionResult GetRoutes([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetRoutes().ToDataSourceResult(request));
        }
        public ActionResult GetDrivers([DataSourceRequest] DataSourceRequest request)
       {
            return Json(GetDrivers().ToDataSourceResult(request));
        }
        public static IEnumerable<Driver> GetDrivers()
        {
            var assign = new Assign();
            return assign.GetDrivers().Select(driver => new Driver
            {
                Driver_Id = driver.Driver_Id,
                Driver_Name = driver.Driver_Name
            });
        }

        public ActionResult GetBuses([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetBuses().ToDataSourceResult(request));
        }

        private static IEnumerable<Bus> GetBuses()
        {
            var assign = new Assign();
            return assign.GetBusses().Select(bus => new Bus
            {
                Bus_Id = bus.Bus_Id,
                Bus_Number = bus.Bus_Number,
                Seat_Capacity = bus.Seat_Capacity
            });
        }

        private static IEnumerable<IndexViewModel> GetAssigned()
        {
            var assign = new Assign();
            return assign.GetAll().Select(current_assign => new IndexViewModel
            {
                Assign_Id = current_assign.Assign_Id,
                Driver_Name = current_assign.Current_Driver.Driver_Name,
                Bus_Number = current_assign.Current_Bus.Bus_Number,
                Seat_Capacity = current_assign.Current_Bus.Seat_Capacity,
                Route_Name = current_assign.Current_Route.Route_Name,
                City = current_assign.Current_Route.City,
                State = current_assign.Current_Route.State,
                Zip = current_assign.Current_Route.Zip,
                StartDateTime = current_assign.StartDateTime,
                EndDateTime = current_assign.EndDateTime
            });
        }
    }
}