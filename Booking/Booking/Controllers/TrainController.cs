using Booking.BusinessLogic.Services;
using Booking.BusinessLogic.ServInterfaces;
using Booking.Helpers;
using Booking.Models.DBModels;
using Booking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Booking.Controllers
{
    public class TrainController : Controller
    {
        private readonly ISqlProcServ sqlProcService;
        private readonly IStationServ stationServ;
        private readonly ICarriageServ carrServ;
        private readonly IReservSeatServ seatServ;
        private readonly IStationOnRouteServ statOnRouteServ;
        private readonly IPersonServ personServ;
        private readonly ITicketServ ticketServ;
        private readonly ObjMapper mapper;
        public List<string> fromTimesToDropDown;

        public TrainController(ISqlProcServ sqlProcServ, IStationServ stationServ, ICarriageServ carrServ, IReservSeatServ seatServ, IStationOnRouteServ statOnRouteServ, IPersonServ personServ, ITicketServ ticketServ)
        {
            this.sqlProcService = sqlProcServ;
            this.stationServ = stationServ;
            this.carrServ = carrServ;
            this.seatServ = seatServ;
            this.personServ = personServ;
            this.statOnRouteServ = statOnRouteServ;
            this.ticketServ = ticketServ;
            mapper = new ObjMapper(this.sqlProcService, this.carrServ, this.seatServ, this.statOnRouteServ, this.personServ, this.ticketServ);
            FillFromTimesDropDown();
        }

        public ActionResult Index()
        {
            ViewBag.FromTimes = new SelectList(fromTimesToDropDown, fromTimesToDropDown[0]);
            return View();
        }

        [HttpPost]
        public ActionResult GetTrains(TrainByStationsAndDate inputs)
        {
            List<TrainByStationsAndDate> trains = mapper.GetTrainsByStationsAndDate(inputs.StartInputStation, inputs.EndInputStation, inputs.DepartDateTime, inputs.FromTime);
            if (trains != null && trains.Count != 0)
            {
                foreach (var train in trains)
                {
                    train.StartInputStation = inputs.StartInputStation;
                    train.EndInputStation = inputs.EndInputStation;
                }
            }
            return PartialView(trains);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void FillFromTimesDropDown()
        {
            fromTimesToDropDown = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                fromTimesToDropDown.Add(i + ":00");
            }
        }

        [HttpPost]
        public JsonResult GetStations(string prefix)
        {
            List<Station> stations = stationServ.GetStationsStartingAtPrefix(prefix);
            if (stations != null && stations.Count != 0)
            {
                List<string> names = stations.Select(s => s.Name).ToList();
                return Json(names, JsonRequestBehavior.AllowGet);
            }
            return Json("");
        }
    }
}