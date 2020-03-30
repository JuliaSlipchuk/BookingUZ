﻿using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.ServInterfaces
{
    public interface IStationOnRouteServ : IBaseService<StationOnRoute>
    {
        StationOnRoute GetByTrainNumbAndStation(int trainNumb, string stationName);
    }
}