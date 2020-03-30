﻿using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.RepoInterfaces
{
    public interface ITrainRepo : IBaseRepository<Train>
    {
        int GetIdByNumber(int trainNumb);
        bool TrainHasThisCarrType(Train train, string carrType);
        DateTime GetDepartDateTime(Train train, DateTime departDate);
        List<int> GetCarrOrderInTrainByCarrType(Train train, int carrTypeID);
    }
}