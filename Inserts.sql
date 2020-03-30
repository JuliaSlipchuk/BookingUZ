USE BookingUZ;

DELETE FROM PersonAccount;
DELETE FROM Ticket;
DELETE FROM Person;
DELETE FROM PersonType;
DELETE FROM StationOnRoute;
DELETE FROM DistanceBetweenStations;
DELETE FROM Station;
DELETE FROM TrainRecurring;
DELETE FROM Train;
DELETE FROM TrainType;
DELETE FROM [Route];
DELETE FROM ReservationSeat;
DELETE FROM Carriage;
DELETE FROM CarriageType;
DELETE FROM Occur;
DELETE FROM [DayOfWeek];


INSERT INTO [Route]
(ID, [Name])
VALUES
(1, 'Kiev-IvanoFrankivsk'), --Kiev-Zhytomyr-Rivne-Lviv-IvanoFrankivsk
(2, 'Rivne-Kiev'), --Rivne-Zhytomyr-Fastiv-Kiev
(3, 'Lviv-Mykolaiv'); --Lviv-Ternopil-Vinnitsa-Mykolaiv

INSERT INTO Station
(ID, [Name], LocationOnMap)
VALUES
(1, 'Kiev', GEOGRAPHY::Point(-50.45, -149.476389, 4326)),
(2, 'Zhytomyr', GEOGRAPHY::Point(-50.254444, -151.342222, 4326)),
(3, 'Rivne', GEOGRAPHY::Point(-50.619722, -153.748611, 4326)),
(4, 'Lviv', GEOGRAPHY::Point(-49.841889, -155.9685, 4326)),
(5, 'IvanoFrankivsk', GEOGRAPHY::Point(-48.922778, -155.289444, 4326)),
(7, 'Fastiv', GEOGRAPHY::Point(-50.074722, -150.081944, 4326)),
(8, 'Ternopil', GEOGRAPHY::Point(-49.566667, -154.4, 4326)),
(9, 'Vinnitsa', GEOGRAPHY::Point(-49.237222, -151.532778, 4326)),
(10, 'Mykolaiv', GEOGRAPHY::Point(-46.975278, -148.006389, 4326));

INSERT INTO StationOnRoute
(ID, RouteID, StationID, [Order])
VALUES
(1, 1, 1, 1),
(2, 1, 2, 2),
(3, 1, 3, 3),
(4, 1, 4, 4),
(5, 1, 5, 5),
(6, 2, 3, 1),
(7, 2, 2, 2),
(8, 2, 7, 3),
(9, 2, 1, 4),
(10, 3, 4, 1),
(11, 3, 8, 2),
(12, 3, 9, 3),
(13, 3, 10, 4);

INSERT INTO TrainType
(ID, [Type], Speed, PriceCoefficient)
VALUES
(1, 'Electricity', 60, 1.1),
(2, 'Night fast', 110, 1.7),
(3, 'Express', 80, 1.5)

INSERT INTO Train
(ID, Number, TrainTypeID, RouteID)
VALUES
(1, 43, 1, 1),
(2, 121, 2, 2),
(3, 198, 2, 3);

INSERT INTO Occur
(ID, Frequency)
VALUES
(1, 'Monthly'),
(2, 'Weekly'),
(3, 'Every day')

INSERT INTO [DayOfWeek]
(ID, [WeekDay])
VALUES
(1, 'Monday'),
(2, 'Tuesday'),
(3, 'Wednesday'),
(4, 'Thursday'),
(5, 'Friday'),
(6, 'Saturday'),
(7, 'Sunday')

INSERT INTO TrainRecurring
(ID, TrainID, OccurID, DayNumbInMonth, DayOfWeekID, DepartureTime, [Description])
VALUES
(1, 1, 2, NULL, 1, '01:10:00', '1'),
(2, 1, 2, NULL, 3, '04:10:00', '2'),
(3, 1, 2, NULL, 5, '12:10:00', '3'),
(4, 2, 1, 5, NULL, '02:20:00', '4'),
(5, 3, 3, NULL, NULL, '07:30:00', '5')

INSERT INTO DistanceBetweenStations
(ID, StationFirstID, StationSecondID, Distance)
VALUES
(1, 1, 2, 200.0),
(2, 2, 3, 200.0),
(3, 3, 4, 300.0),
(4, 4, 5, 200.0),
(5, 3, 2, 180),
(6, 2, 7, 130.0),
(7, 7, 1, 60.0),
(8, 4, 8, 260.0),
(9, 8, 9, 230.0),
(10, 9, 10, 440.0)

INSERT INTO CarriageType
(ID, [Type], CountOfSeats, PriceCoefficient)
VALUES
(1, 'Seatpost', 54, 1.3),
(2, 'Seating', 80, 1.1),
(3, 'Compartment', 36, 1.6),
(4, 'Suite', 16, 2.2)

INSERT INTO Carriage
(ID, [Order], TrainID, CarriageTypeID)
VALUES
(1, 1, 1, 1),
(2, 2, 1, 1),
(3, 3, 1, 1),
(4, 4, 1, 3),
(5, 5, 1, 3),
(6, 6, 1, 4),
(7, 1, 2, 2),
(8, 2, 2, 2),
(9, 3, 2, 2),
(10, 4, 2, 2),
(11, 5, 2, 2),
(12, 6, 2, 2),
(13, 7, 2, 2),
(14, 1, 3, 3),
(15, 2, 3, 3),
(16, 3, 3, 3),
(17, 4, 3, 4),
(18, 5, 3, 4)

INSERT INTO ReservationSeat
(ID, Number, CarriageID)
VALUES
(1, 1, 1),
(2, 3, 1),
(3, 5, 1),
(4, 10, 2),
(5, 20, 2),
(6, 50, 3),
(7, 22, 4),
(8, 20, 4),
(9, 1, 6),
(10, 2, 6),
(11, 20, 6),
(12, 89, 11),
(13, 5, 11),
(14, 6, 12),
(15, 8, 12),
(16, 10, 12),
(17, 30, 14),
(18, 33, 14),
(19, 8, 15),
(20, 1, 17),
(21, 7, 18),
(22, 3, 6),
(23, 30, 4);

INSERT INTO PersonType
(ID, [Type], PriceCoefficient)
VALUES
(1, 'Ordinary', 1.7),
(2, 'Disabled', 1.2),
(3, 'Student', 1.3),
(4, 'Pensioner', 1.4),
(5, 'Child', 1.1)

INSERT INTO Person
(ID, FirstName, LastName, BirthDate, PersonTypeID, Phone, Email)
VALUES
(1, 'Oleksandr', 'Klinkovich', '2000-05-05', 1, '+38-067-852-96-96', 'olekKlin@gmail.com'),
(2, 'Mikhail', 'Kolesnikov', '1996-02-06', 2, '+38-068-741-40-25', 'mishaKoleso@ukr.net'),
(3, 'Olga', 'Drugaia', '1963-01-08', 4, '+38-096-412-85-96', 'olgaaaa@gmail.com'),
(4, 'Maria', 'Penkivska', '2001-11-08', 3, '+38-097-410-50-80', 'penkivska@meta.ua'),
(5, 'Eugen', 'Zarembovskiy', '1960-01-01', 4, '+38-096-856-41-82', 'eugenTheBest@ukr.net'),
(6, 'Taras', 'Vons', '1999-10-10', 3, '+38-066-380-70-52', 'taric@univ.kiev.ua'),
(7, 'Yuliia', 'Slipchuk', '2000-01-01', 3, '+38-068-105-52-84', 'julia2015olex@gmail.com'),
(8, 'Andriy', 'Kravets', '2018-12-30', 5, '+38-044-852-02-03', 'iAmChild@meta.ua'),
(9, 'Oksana', 'Zabuzko', '2019-11-11', 5, '+38-067-200-10-10', 'iAmChildToo@gmail.com')

INSERT INTO PersonAccount
(ID, PersonID, [Login], [Password])
VALUES
(1, 1, 'olekKlink', 'myPass123'),
(2, 4, 'masha007', 'helloWorld456'),
(3, 6, 'taric', 'knuTheBest')

INSERT INTO Ticket
(ID, PersonID, StationOnRouteStartID, StationOnRouteEndID, ReservationSeatID, DepartureDateTime, ArrivalDateTime, Price)
VALUES
(1, 1, 1, 3, 1, '2020-02-24 01:10:00', NULL, NULL),
(2, 1, 4, 5, 2, '2020-02-26 04:10:00', NULL, NULL),
(3, 2, 1, 5, 3, '2020-02-28 12:10:00', NULL, NULL),
(4, 3, 2, 4, 4, '2020-03-02 01:10:00', NULL, NULL),
(5, 3, 3, 5, 5, '2020-03-04 04:10:00', NULL, NULL),
(6, 3, 1, 5, 6, '2020-03-06 12:10:00', NULL, NULL),
(7, 4, 1, 2, 7, '2020-03-06 12:10:00', NULL, NULL),
(8, 5, 3, 5, 8, '2020-03-04 04:10:00', NULL, NULL),
(9, 5, 2, 5, 9, '2020-03-13 12:10:00', NULL, NULL),
(10, 6, 2, 4, 10, '2020-02-24 01:10:00', NULL, NULL),
(11, 6, 1, 3, 11, '2020-02-28 12:10:00', NULL, NULL),
(12, 6, 6, 8, 12, '2020-03-05 02:20:00', NULL, NULL),
(13, 7, 7, 9, 13, '2020-03-05 02:20:00', NULL, NULL),
(14, 7, 7, 9, 14, '2020-03-05 02:20:00', NULL, NULL),
(15, 7, 6, 7, 15, '2020-04-05 02:20:00', NULL, NULL),
(16, 7, 7, 8, 16, '2020-04-05 02:20:00', NULL, NULL),
(17, 7, 10, 12, 17, '2020-03-03 07:30:00', NULL, NULL),
(18, 8, 10, 13, 18, '2020-03-05 07:30:00', NULL, NULL),
(19, 8, 12, 13, 19, '2020-03-03 07:30:00', NULL, NULL),
(20, 8, 12, 13, 20, '2020-03-07 07:30:00', NULL, NULL),
(21, 9, 10, 13, 21, '2020-03-07 07:30:00', NULL, NULL),
(22, 9, 1, 2, 22, '2020-02-24 01:10:00', NULL, NULL),
(23, 9, 1, 2, 23, '2020-02-24 01:10:00', NULL, NULL);