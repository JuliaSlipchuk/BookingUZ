USE BookingUZ;

DROP TABLE PersonAccount;
DROP TABLE Ticket;
DROP TABLE Person;
DROP TABLE PersonType;
DROP TABLE StationOnRoute;
DROP TABLE DistanceBetweenStations;
DROP TABLE Station;
DROP TABLE TrainRecurring;
DROP TABLE Train;
DROP TABLE TrainType;
DROP TABLE [Route];
DROP TABLE ReservationSeat;
DROP TABLE Carriage;
DROP TABLE CarriageType;
DROP TABLE Occur;
DROP TABLE [DayOfWeek];

IF OBJECT_ID('dbo.Station') IS NULL
	BEGIN
		CREATE TABLE Station
		(
			ID INT PRIMARY KEY,
			[Name] NVARCHAR(200) UNIQUE NOT NULL, -- search stations by name
			LocationOnMap GEOGRAPHY NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.[Route]') IS NULL 
	BEGIN
		CREATE TABLE [Route]
		(
			ID INT PRIMARY KEY,
			[Name] NVARCHAR(200) UNIQUE NOT NULL, -- search route by name
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.DistanceBetweenStations') IS NULL
	BEGIN
		CREATE TABLE DistanceBetweenStations
		(
			ID INT PRIMARY KEY,
			StationFirstID INT NOT NULL REFERENCES Station(ID),
			StationSecondID INT NOT NULL REFERENCES Station(ID),
			Distance REAL NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		)

		ALTER TABLE DistanceBetweenStations
		ADD CONSTRAINT Check_DistanceBtwStations
		CHECK (Distance >= 0.5)
	END;

IF OBJECT_ID('dbo.StationOnRoute') IS NULL 
	AND OBJECT_ID('dbo.[Route]') IS NOT NULL
	AND OBJECT_ID('dbo.Station') IS NOT NULL
	BEGIN
		CREATE TABLE StationOnRoute
		(
			ID INT PRIMARY KEY,
			RouteID INT NOT NULL REFERENCES [Route](ID),
			StationID INT REFERENCES Station(ID) NOT NULL,
			[Order] INT NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE StationOnRoute
		ADD CONSTRAINT Check_Order1
		CHECK ([Order] >= 1 AND [Order] <= 100);
	END;

IF OBJECT_ID('dbo.TrainType') IS NULL
	BEGIN
		CREATE TABLE TrainType
		(
			ID INT PRIMARY KEY,
			[Type] NVARCHAR(200) NOT NULL,
			Speed INT NOT NULL,
			PriceCoefficient REAL NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE TrainType
		ADD CONSTRAINT Check_Speed
		CHECK (Speed >= 30 AND Speed <= 220);

		ALTER TABLE TrainType
		ADD CONSTRAINT Check_PriceCoeffTrain
		CHECK (PriceCoefficient >= 1.075 AND PriceCoefficient <= 2.7)
	END;

IF OBJECT_ID('dbo.Train') IS NULL
	AND OBJECT_ID('dbo.[Route]') IS NOT NULL
	BEGIN
		CREATE TABLE Train
		(
			ID INT PRIMARY KEY,
			Number INT UNIQUE NOT NULL, -- search train by number
			TrainTypeID INT NOT NULL REFERENCES TrainType(ID),
			RouteID INT NOT NULL REFERENCES [Route](ID),
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE Train
		ADD CONSTRAINT Check_TrainNumb
		CHECK (Number >= 1 AND Number <= 999)
	END;

IF OBJECT_ID('dbo.Occur') IS NULL
	BEGIN
		CREATE TABLE Occur
		(
			ID INT PRIMARY KEY,
			Frequency NVARCHAR(100) NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.[DayOfWeek]') IS NULL
	BEGIN
		CREATE TABLE [DayOfWeek]
		(
			ID INT PRIMARY KEY,
			[WeekDay] NVARCHAR(50) NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.TrainRecurring') IS NULL 
	AND OBJECT_ID('dbo.Train') IS NOT NULL
	BEGIN
		CREATE TABLE TrainRecurring
		(
			ID INT PRIMARY KEY,
			TrainID INT NOT NULL REFERENCES Train(ID),
			OccurID INT NOT NULL REFERENCES Occur(ID),
			DayNumbInMonth INT NULL, --if Occur == monthly
			DayOfWeekID INT NULL REFERENCES [DayOfWeek](ID), --if Occur == weekly
			DepartureTime TIME NOT NULL,
			[Description] NVARCHAR(MAX) NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE TrainRecurring 
		ADD CONSTRAINT Check_DayNumbInMonth
		CHECK (DayNumbInMonth >= 1 AND DayNumbInMonth <= 29)

	END;

IF OBJECT_ID('dbo.CarriageType') IS NULL
	BEGIN
		CREATE TABLE CarriageType
		(
			ID INT PRIMARY KEY,
			[Type] NVARCHAR(200) NOT NULL,
			CountOfSeats INT NOT NULL,
			PriceCoefficient REAL NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE CarriageType
		ADD CONSTRAINT Check_CountOfSeats
		CHECK (CountOfSeats >= 2 AND CountOfSeats <= 100)

		ALTER TABLE CarriageType
		ADD CONSTRAINT Check_PriceCoeffCarr
		CHECK (PriceCoefficient >= 1.075 AND PriceCoefficient <= 2.7)
	END;

IF OBJECT_ID('dbo.Carriage') IS NULL
	AND OBJECT_ID('dbo.Train') IS NOT NULL
	AND OBJECT_ID('dbo.CarriageType') IS NOT NULL
	BEGIN
		CREATE TABLE Carriage
		(
			ID INT PRIMARY KEY,
			[Order] INT NOT NULL,
			TrainID INT NOT NULL REFERENCES Train(ID),
			CarriageTypeID INT NOT NULL REFERENCES CarriageType(ID),
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE Carriage
		ADD CONSTRAINT Check_CarrNumb
		CHECK ([Order] >= 1 AND [Order] <= 50);
	END;

IF OBJECT_ID('dbo.ReservationSeat') IS NULL
	AND OBJECT_ID('dbo.Carriage') IS NOT NULL
	BEGIN
		CREATE TABLE ReservationSeat
		(
			ID INT PRIMARY KEY,
			Number INT NOT NULL,
			CarriageID INT NOT NULL REFERENCES Carriage(ID),
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.PersonType') IS NULL
	BEGIN
		CREATE TABLE PersonType
		(
			ID INT PRIMARY KEY,
			[Type] NVARCHAR(100) NOT NULL,
			PriceCoefficient REAL NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE PersonType
		ADD CONSTRAINT Check_PriceCoeffPerson
		CHECK (PriceCoefficient >= 1.1 AND PriceCoefficient <= 2.0)
	END;

IF OBJECT_ID('dbo.Person') IS NULL
   AND OBJECT_ID('dbo.PersonType') IS NOT NULL
	BEGIN
		CREATE TABLE Person
		(
			ID INT PRIMARY KEY,
			FirstName NVARCHAR(100) NOT NULL,
			LastName NVARCHAR(200) NOT NULL,
			BirthDate DATE NOT NULL,
			PersonTypeID INT NOT NULL REFERENCES PersonType(ID),
			Phone NVARCHAR(20) NOT NULL,
			Email NVARCHAR(100) NOT NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE Person
		ADD CONSTRAINT Check_BirthDate
		CHECK (BirthDate >= DATEADD(yy, -120, GETDATE()) AND BirthDate <= GETDATE());
	END;

IF OBJECT_ID('dbo.PersonAccount') IS NULL 
	AND OBJECT_ID('dbo.Person') IS NOT NULL
	BEGIN
		CREATE TABLE PersonAccount
		(
			ID INT PRIMARY KEY,
			[Login] NVARCHAR(200) UNIQUE NOT NULL, -- search person by login
			[Password] NVARCHAR(200) NOT NULL,
			PersonID INT NOT NULL REFERENCES Person(ID),
			IsDeleted BIT DEFAULT 0 NOT NULL
		);
	END;

IF OBJECT_ID('dbo.Ticket') IS NULL 
	AND OBJECT_ID('dbo.Person') IS NOT NULL
	AND OBJECT_ID('dbo.Train') IS NOT NULL
	AND OBJECT_ID('dbo.ReservationSeat') IS NOT NULL
	AND OBJECT_ID('dbo.StationOnRoute') IS NOT NULL
	BEGIN
		CREATE TABLE Ticket
		(
			ID INT PRIMARY KEY,
			PersonID INT NOT NULL REFERENCES Person(ID),
			StationOnRouteStartID INT NOT NULL REFERENCES StationOnRoute(ID),
			StationOnRouteEndID INT NOT NULL REFERENCES StationOnRoute(ID),
			ReservationSeatID INT NOT NULL REFERENCES ReservationSeat(ID),
			DepartureDateTime DATETIME NOT NULL,
			ArrivalDateTime DATETIME NULL,
			Price MONEY DEFAULT 0 NULL,
			IsDeleted BIT DEFAULT 0 NOT NULL
		);

		ALTER TABLE Ticket
		ADD CONSTRAINT Check_Price
		CHECK (Price >= 5 AND Price <= 5000);
	END;
