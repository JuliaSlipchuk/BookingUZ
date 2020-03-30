using Booking.BusinessLogic.ServInterfaces;
using Booking.DataAccess.RepoInterfaces;
using Booking.DataAccess.Repositories;
using Booking.Models.DBModels;
using System.Collections.Generic;

namespace Booking.BusinessLogic.Services
{
    public class CarriageService : ICarriageServ
    {
        private readonly ICarriageRepo carriageRepo;
        public CarriageService(ICarriageRepo carriageRepo)
        {
            this.carriageRepo = carriageRepo;
        }
        public void Create (Carriage carr)
        {
            carriageRepo.Create(carr);
        }
        public void Update (Carriage carr)
        {
            carriageRepo.Update(carr);
        }
        public void Delete (int? carrId)
        {
            carriageRepo.Delete(carrId);
        }

        public List<Carriage> GetAllItems()
        {
            return carriageRepo.GetAllItems();
        }
        public Carriage GetItemById(int? id)
        {
            return carriageRepo.GetItemById(id);
        }

        public int GetCarrTypeIdByCarrType(string carrType)
        {
            return carriageRepo.GetCarrTypeIdByCarrType(carrType);
        }

        public int GetCountOfSeatsByCarrType(string carrType)
        {
            return carriageRepo.GetCountOfSeatsByCarrType(carrType);
        }

        public string GetCarrTypeByCarrOrderInTrain(int trainNumb, int carrOrder)
        {
            return carriageRepo.GetCarrTypeByCarrOrderInTrain(trainNumb, carrOrder);
        }

        public CarriageType GetCarrTypyById(int id)
        {
            return carriageRepo.GetCarrTypyById(id);
        }

        public Carriage GetCarrByTypeOrderAndTrain(int carrTypeId, int order, int trainNumb)
        {
            return carriageRepo.GetCarrByTypeOrderAndTrain(carrTypeId, order, trainNumb);
        }
    }
}
