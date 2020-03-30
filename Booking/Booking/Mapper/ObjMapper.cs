using Booking.BusinessLogic.ServInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Booking.Models.ModelsReturnFromProc;
using Booking.ViewModels;
using Booking.Models.DBModels;
using Booking.ViewModelsAndSubViewModels;

namespace Booking.Helpers
{
    public class ObjMapper : IMapper
    {
        private ISqlProcServ sqlProcService { get; }
        private ICarriageServ carrServ { get; }
        private IReservSeatServ seatServ { get; }
        private IStationOnRouteServ statOnRouteServ { get; }
        private IPersonServ personServ { get; }
        private ITicketServ ticketServ { get; }
        MapperConfiguration config { get; set; }
        AutoMapper.IMapper mapper { get; set; }

        public ObjMapper(ISqlProcServ sqlProcServ, ICarriageServ carrServ, IReservSeatServ seatServ, IStationOnRouteServ statOnRouteServ, IPersonServ personServ, ITicketServ ticketServ)
        {
            this.seatServ = seatServ;
            this.sqlProcService = sqlProcServ;
            this.carrServ = carrServ;
            this.statOnRouteServ = statOnRouteServ;
            this.personServ = personServ;
            this.ticketServ = ticketServ;

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TrainByTwoStationsAndDate, TrainByStationsAndDate>();
                cfg.CreateMap<CarriageTypeInTrain, CarrTypesAndCountOfSeats>();
                cfg.CreateMap<FreeSeatInCarr, FreeSeatInCarriage>();
                cfg.AllowNullCollections = true;
            });
            mapper = config.CreateMapper();
        }

        public List<TrainByStationsAndDate> GetTrainsByStationsAndDate(string firstStat, string seconsStat, DateTime date, TimeSpan fromTime)
        {
            List<TrainByTwoStationsAndDate> trains;
            List<CarriageTypeInTrain> types;
            sqlProcService.GetTrainByStationsAndDate(firstStat, seconsStat, date, fromTime, out trains, out types);
            List<TrainByStationsAndDate> result = mapper.Map<List<TrainByTwoStationsAndDate>, List<TrainByStationsAndDate>>(trains);
            for(int i = 0; i < result.Count; i++)
            {
                result[i].CarriageTypes = new List<CarrTypesAndCountOfSeats>();
                List<CarriageTypeInTrain> carrTypesInRes = types.Where(t => t.TrainNumber == result[i].TrainNumber).ToList();
                result[i].CarriageTypes.AddRange(mapper.Map<List<CarriageTypeInTrain>, List<CarrTypesAndCountOfSeats>>(carrTypesInRes));
            }
            return result;
        }
        public List<CarrTypesAndCountOfSeats> GetCarrTypesInTrain(string firstStat, string seconsStat, DateTime date, TimeSpan fromTime)
        {
            List<TrainByTwoStationsAndDate> trains;
            List<CarriageTypeInTrain> types;
            sqlProcService.GetTrainByStationsAndDate(firstStat, seconsStat, date, fromTime, out trains, out types);
            return mapper.Map<List<CarriageTypeInTrain>, List<CarrTypesAndCountOfSeats>>(types);
        }
        public List<FreeSeatInCarriage> GetFreeSeatsInCarriages(int trainID, DateTime departDateTime, int carrTypeID, string startStatName, string endStatName)
        {
            List<FreeSeatInCarriage> result = new List<FreeSeatInCarriage>();
            List<FreeSeatInCarr> freeSeatsFrModel = sqlProcService.GetFreeSeatsInCarrs(trainID, departDateTime, carrTypeID, startStatName, endStatName);
            List<int> carrOrders = freeSeatsFrModel.Select(co => co.CarriageOrder).Distinct().ToList();
            for (int i = 0; i < carrOrders.Count; i++)
            {
                result.Add(new FreeSeatInCarriage { CarriageOrder = carrOrders[i], SeatNumbers = freeSeatsFrModel.Where(fs => fs.CarriageOrder == carrOrders[i]).Select(fs=>fs.SeatNumber).ToList() });
            }
            return result;
        }

        public void CreateNewTickets(ref List<TicketVM> tickets, InformAboutPassengers personsInf)
        {
            CarriageType carrType = carrServ.GetCarrTypyById(carrServ.GetCarrTypeIdByCarrType(tickets[0].CarriageType));
            Carriage carr = carrServ.GetCarrByTypeOrderAndTrain(carrType.ID, tickets[0].CarriageOrder, tickets[0].TrainNumber);
            StationOnRoute startStatOnRoute = statOnRouteServ.GetByTrainNumbAndStation(tickets[0].TrainNumber, tickets[0].StartStation);
            StationOnRoute endStatOnRoute = statOnRouteServ.GetByTrainNumbAndStation(tickets[0].TrainNumber, tickets[0].EndStation);
            ReservationSeat seat;
            Person person;
            bool haveTea = false;
            bool haveBed = false;

            for (int i = 0; i < tickets.Count(); i++)
            {
                int maxIdInResrvSeat = seatServ.GetMaxId();
                seat = new ReservationSeat() { ID = maxIdInResrvSeat + 1, Number = tickets[i].SeatNumber, CarriageID = carr.ID, IsDeleted = false };
                seatServ.Create(seat);
                if (!personServ.PersonExist(tickets[i].FirstName, tickets[i].LastName, tickets[i].BirthDate, tickets[i].PersonType))
                {
                    int maxIdInPerson = personServ.GetMaxId();
                    int personTypeId = personServ.GetPersonTypeIdByType(tickets[i].PersonType);
                    person = new Person() { ID = maxIdInPerson + 1, FirstName = tickets[i].FirstName, LastName = tickets[i].LastName, BirthDate = tickets[i].BirthDate, Email = personsInf.Email[i], Phone = personsInf.Phone[i], PersonTypeID = personTypeId, IsDeleted = false };
                    personServ.Create(person);
                }
                else
                {
                    person = personServ.GetPersonByFLNameBirthType(tickets[i].FirstName, tickets[i].LastName, tickets[i].BirthDate, tickets[i].PersonType);
                }
                int maxIdInTicket = ticketServ.GetMaxId();
                Ticket ticket = new Ticket() { ID = maxIdInTicket + 1, PersonID = person.ID, DepartureDateTime = tickets[i].DepartureDateTime, ArrivalDateTime = tickets[i].ArrivalDateTime, ReservationSeatID = seat.ID, StationOnRouteStartID = startStatOnRoute.ID, StationOnRouteEndID = endStatOnRoute.ID, IsDeleted = false };
                ticketServ.Create(ticket);
                if (tickets[i].HaveTea)
                {
                    haveTea = true;
                }
                if (tickets[i].HaveBed)
                {
                    haveBed = true;
                }
                tickets[i].Price = sqlProcService.GetTicketPrice(ticket.ID, haveTea, haveBed);
                ticketServ.AddPriceForTicket(ticket.ID, tickets[i].Price);
                haveTea = false;
                haveBed = false;
            }
        }
    }
}