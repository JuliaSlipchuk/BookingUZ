using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.BusinessLogic.Services
{
    public class ReservationSeatService : IReservSeatServ
    {
        private readonly IReservSeatRepo reservSeatRepo;

        public ReservationSeatService(IReservSeatRepo reservSeatRepo)
        {
            this.reservSeatRepo = reservSeatRepo;
        }

        public void Create(ReservationSeat resSeat)
        {
            reservSeatRepo.Create(resSeat);
        }
        public void Update(ReservationSeat resSeat)
        {
            reservSeatRepo.Update(resSeat);
        }
        public void Delete(int? resSeatId)
        {
            reservSeatRepo.Delete(resSeatId);
        }

        public List<ReservationSeat> GetAllItems()
        {
            return reservSeatRepo.GetAllItems();
        }
        public ReservationSeat GetItemById(int? id)
        {
            return reservSeatRepo.GetItemById(id);
        }

        public int GetMaxId()
        {
            return reservSeatRepo.GetMaxId();
        }
    }
}
