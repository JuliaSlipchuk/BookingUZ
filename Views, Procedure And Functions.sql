USE BookingUZ

CREATE OR ALTER VIEW AllTrainsWithStations -- вибрати всі потяги, маршрути і їхні станції
AS
SELECT Train.Number AS TrainNumb, [Route].[Name] AS RouteName, Station.[Name] AS StationName FROM Train
	INNER JOIN [Route] ON Train.RouteID = [Route].ID
	INNER JOIN StationOnRoute ON [Route].ID = StationOnRoute.RouteID
	INNER JOIN Station ON StationOnRoute.StationID = Station.ID;

CREATE OR ALTER VIEW PeopleAndAccountsAndTickets -- вибрати людину, її квитки на потяг, час, місце для нагадувань 
AS
SELECT P.ID AS PersonId, TR.Number AS TrainNumb, R.[Name] AS RouteName, SS.[Name] AS [Start], SE.[Name] AS [End], RS.ID AS ReservSeat FROM Person AS P
		LEFT JOIN Ticket AS T ON P.ID = T.PersonID
		INNER JOIN Station AS SS ON T.StationOnRouteStartID = SS.ID
		INNER JOIN Station AS SE ON T.StationOnRouteEndID = SE.ID
		INNER JOIN  ReservationSeat AS RS ON T.ReservationSeatID = RS.ID
		INNER JOIN Carriage AS C ON RS.CarriageID = C.ID
		INNER JOIN Train AS TR ON C.TrainID = Tr.ID
		INNER JOIN [Route] AS R ON TR.RouteID = R.ID;

CREATE OR ALTER FUNCTION GetDistanceBetweenStations (@StatStartID INT, @StatEndID INT, @RouteID INT)
RETURNS REAL
BEGIN
	DECLARE @Distance REAL;
	DECLARE @FirstOrder INT;
	DECLARE @SecondOrder INT;

	SET @FirstOrder = (SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.StationID = 3 AND SOR1.RouteID = 2);
	SET @SecondOrder = (SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.StationID = 7 AND SOR1.RouteID = 2);

	SET @Distance =	(SELECT SUM(DBS.Distance)
						FROM StationOnRoute AS A 
						LEFT JOIN StationOnRoute AS B ON A.RouteID = B.RouteID
						LEFT JOIN DistanceBetweenStations AS DBS ON A.StationID = DBS.StationFirstID AND B.StationID = DBS.StationSecondID 
						WHERE A.RouteID = 2 AND A.[Order] >= @FirstOrder AND B.[Order] <= @SecondOrder AND A.[Order] + 1 = B.[Order]);
	RETURN @Distance;
END;

CREATE OR ALTER FUNCTION PriceCalculation (@TicketID INT)
RETURNS MONEY
AS
BEGIN
	DECLARE @Price MONEY;

	DECLARE @CarrId INT;
	SET @CarrId = (SELECT TOP 1 Carr.ID FROM Carriage AS Carr
				  INNER JOIN ReservationSeat AS ResSeat ON Carr.ID = ResSeat.CarriageID
				  WHERE ResSeat.ID = 
					 (SELECT TOP 1 RS.ID FROM ReservationSeat AS RS WHERE RS.ID = 
						(SELECT TOP 1 T.ReservationSeatID FROM Ticket AS T WHERE T.ID = @TicketID)));
	DECLARE @CarrPriceCoeff REAL;
	SET @CarrPriceCoeff = (SELECT CarrType.PriceCoefficient FROM CarriageType AS CarrType 
						  INNER JOIN Carriage AS Carr ON CarrType.ID = Carr.CarriageTypeID 
							  WHERE Carr.ID = @CarrId);

	DECLARE @TrainID INT;
	SET @TrainID = (SELECT T.ID FROM Train AS T 
						WHERE T.ID = (SELECT Carr.TrainID FROM Carriage AS Carr WHERE Carr.ID = @CarrId));
	DECLARE @TrainPriceCoeff REAL;
	SET @TrainPriceCoeff = (SELECT TrType.PriceCoefficient FROM TrainType AS TrType 
								WHERE TrType.ID = (SELECT T.TrainTypeID FROM Train AS T WHERE T.ID = @TrainID));

	DECLARE @PersonPriceCoeff REAL;
	SET @PersonPriceCoeff = (SELECT PerType.PriceCoefficient FROM PersonType AS PerType WHERE PerType.ID = 
								(SELECT Per.PersonTypeID FROM Person AS Per WHERE Per.ID = 
									(SELECT T.PersonID FROM Ticket AS T WHERE T.ID = @TicketID)));

	DECLARE @StatStartID INT;
	DECLARE @StatEndID INT;
	SET @StatStartID = (SELECT StatOnRoute.StationID FROM StationOnRoute AS StatOnRoute WHERE StatOnRoute.ID = 
							  (SELECT T.StationOnRouteStartID FROM Ticket AS T WHERE T.ID = @TicketID));
	SET @StatEndID = (SELECT StatOnRoute.StationID FROM StationOnRoute AS StatOnRoute WHERE StatOnRoute.ID = 
							(SELECT T.StationOnRouteEndID FROM Ticket AS T WHERE T.ID = @TicketID));

	DECLARE @RouteId INT;
	SET @RouteId = (SELECT R.ID FROM [Route] AS R WHERE R.ID = (SELECT T.RouteID FROM Train AS T WHERE T.ID = @TrainID));

	DECLARE @Distance REAL;
	SET @Distance = 0.0;
	EXEC @Distance = GetDistanceBetweenStations @StatStartID, @StatEndID, @RouteId;

	SET @Price = @CarrPriceCoeff * @TrainPriceCoeff * @PersonPriceCoeff * @Distance;

	RETURN @Price;
END;

CREATE OR ALTER FUNCTION ArrivalDateTimeCalculation (@DepartureDateTime DATETIME, @TrainId INT, @StartStationId INT, @EndStationId INT)
RETURNS DATETIME
AS
BEGIN
	DECLARE @ArrivalDateTime DATETIME;

	DECLARE @Speed INT;
	SET @Speed = (SELECT TrainType.Speed FROM TrainType WHERE TrainType.ID = 
					 (SELECT Train.TrainTypeID FROM Train WHERE Train.ID = @TrainId));

	DECLARE @RouteId INT;
	SET @RouteId = (SELECT R.ID FROM [Route] AS R WHERE R.ID = 
						(SELECT T.RouteID FROM Train AS T WHERE T.ID = @TrainId));

	DECLARE @Distance REAL;
	EXEC @Distance = GetDistanceBetweenStations @StartStationId, @EndSTationId, @RouteId;

	DECLARE @TimeForTravel REAL;
	SET @TimeForTravel = @Distance / @Speed;

	DECLARE @Hours INT;
	SET @Hours = FLOOR(@TimeForTravel);
	DECLARE @Minutes INT;
	SET @Minutes = (ABS(@TimeForTravel) - FLOOR(@TimeForTravel)) * 60;

	SET @ArrivalDateTime = (SELECT DATEADD(HOUR, @Hours, @DepartureDateTime));
	SET @ArrivalDateTime = (SELECT DATEADD(MINUTE, @Minutes, @ArrivalDateTime));

	RETURN @ArrivalDateTime;
END;

SELECT dbo.ArrivalDateTimeCalculation ('02.24.2020 01:10:00', 1, 1, 2);

CREATE OR ALTER PROCEDURE GetTrainByTwoStationsAndDate (@From INT, @To INT, @Date DATE, @FromTime TIME)
AS 
BEGIN
	 IF OBJECT_ID('tempdb..#Trains') IS NOT NULL
	 BEGIN
		DROP TABLE #Trains;
	 END;
	 IF OBJECT_ID('tempdb..#Routes') IS NOT NULL
	 BEGIN
		DROP TABLE #Routes;
	 END;
	 IF OBJECT_ID('temp..#TrainsByTwoStationsDate') IS NOT NULL
	 BEGIN
		DROP TABLE #TrainsByTwoStationsDate;
	 END;
	 
	 (SELECT TR.TrainID AS ID, R.ID AS RouteID, CAST(@Date AS DATETIME) + CAST(TR.DepartureTime AS DATETIME) AS DepartDateTime
	  INTO #Trains
	  FROM TrainRecurring AS TR
	  INNER JOIN Train AS T ON T.ID = TR.TrainID
	  INNER JOIN [Route] AS R ON R.ID = T.RouteID
      WHERE (TR.OccurID = 1 AND DATEPART(DAY, @Date) = TR.DayNumbInMonth) 
			OR TR.OccurID = 3 OR 
			(TR.OccurID = 2 AND DATENAME(WEEKDAY, @Date) = (SELECT DOW.[WeekDay] FROM [DayOfWeek] AS DOW WHERE DOW.ID = TR.DayOfWeekID))
			AND TR.DepartureTime >= @FromTime);
	 
	(SELECT DISTINCT BaseStatOnRoute.RouteID AS ID
	 INTO #Routes FROM StationOnRoute AS BaseStatOnRoute
			WHERE (SELECT [Start].[Order] FROM StationOnRoute AS [Start] WHERE [Start].StationID = @From AND [Start].RouteID = BaseStatOnRoute.RouteID) < 
				(SELECT [Finish].[Order] FROM StationOnRoute AS [Finish] WHERE [Finish].StationID = @To AND [Finish].RouteID = BaseStatOnRoute.RouteID));

	SELECT T.ID AS TrainID, [Start].[Name] AS StartStation, [End].[Name] AS EndStation, TTemp.DepartDateTime AS DepartDateTime, dbo.ArrivalDateTimeCalculation (TTemp.DepartDateTime, T.ID, [Start].[ID], [End].[ID]) AS ArrivalDateTime, CAST(dbo.ArrivalDateTimeCalculation (TTemp.DepartDateTime, T.ID, [Start].[ID], [End].[ID]) - TTemp.DepartDateTime AS TIME) AS Duration
		   INTO #TrainsByTwoStationsDate FROM #Trains AS TTemp 
		   INNER JOIN Train AS T ON T.ID = TTemp.ID
		   INNER JOIN Station AS [Start] ON [Start].ID = 
				(SELECT SOR1.StationID FROM StationOnRoute AS SOR1 WHERE SOR1.RouteID = T.RouteID AND SOR1.[Order] = 
					(SELECT MIN([Order]) FROM StationOnRoute WHERE StationOnRoute.RouteID = T.RouteID))
		   INNER JOIN Station AS [End] ON [End].ID = 
				(SELECT SOR2.StationID FROM StationOnRoute AS SOR2 WHERE SOR2.RouteID = T.RouteID AND SOR2.[Order] = 
					(SELECT MAX([Order]) FROM StationOnRoute WHERE StationOnRoute.RouteID = T.RouteID))
		   INNER JOIN #Routes AS RTemp ON TTemp.RouteID = RTemp.ID;
	
	IF OBJECT_ID('temp..#CarrTypesInTrains') IS NOT NULL
	BEGIN 
		DROP TABLE #CarrTypesInTrains;
	END;
	CREATE TABLE #CarrTypesInTrains
	(
		ID INT IDENTITY,
		TrainID INT,
		CarrTypeID INT,
		SeatsCount INT
	);

	INSERT INTO #CarrTypesInTrains(TrainID, CarrTypeID, SeatsCount)
	SELECT DISTINCT TR.ID AS IDTrain, CRTP.ID AS IDCarrType, 0
		   FROM Train AS TR
		   INNER JOIN Carriage AS C ON C.TrainID = TR.ID
		   INNER JOIN CarriageType AS CRTP ON C.CarriageTypeID = CRTP.ID
		   WHERE TR.ID IN (SELECT TTemp.TrainID FROM #TrainsByTwoStationsDate AS TTemp);
	
	IF OBJECT_ID('temp..#Temp') IS NOT NULL
	BEGIN 
		DROP TABLE #Temp;
	END;
	CREATE TABLE #Temp
	(
		SeatNumber INT,
		CarrOrder INT
	);

	DECLARE @CountRows INT = (SELECT COUNT(*) FROM #CarrTypesInTrains);
	WHILE @CountRows > 0
	BEGIN
		DECLARE @TrainID INT = (SELECT #CarrTypesInTrains.TrainID FROM #CarrTypesInTrains WHERE ID = @CountRows);
		DECLARE @CarrTypeID INT = (SELECT #CarrTypesInTrains.CarrTypeID FROM #CarrTypesInTrains WHERE ID = @CountRows);
		INSERT INTO #Temp EXEC GetFreeSeats @TrainID, @Date, @CarrTypeID, @From, @To;
		DECLARE @Count INT = (SELECT COUNT(*) FROM #Temp);
		UPDATE #CarrTypesInTrains 
		SET SeatsCount = @Count WHERE ID = @CountRows;
		SET @CountRows = @CountRows - 1;
		DELETE FROM #Temp;
	END;

	SELECT T.Number AS TrainNumber, TBSD.StartStation AS StartStation, TBSD.EndStation AS EndStation, TBSD.DepartDateTime AS DepartDateTime, TBSD.ArrivalDateTime AS ArrivalDateTime, TBSD.Duration
		   FROM Train AS T
		   INNER JOIN #TrainsByTwoStationsDate AS TBSD ON TBSD.TrainID = T.ID;

	SELECT T.Number AS TrainNumber, CT.[Type] AS CarrType, CTT.SeatsCount AS CountOfSeats
		   FROM #CarrTypesInTrains AS CTT
		   INNER JOIN Train AS T ON T.ID = CTT.TrainID
		   INNER JOIN CarriageType AS CT ON CT.ID = CTT.CarrTypeID;
END;

EXEC GetTrainByTwoStationsAndDate 1, 2, '2020-02-24', '00:00:00';

CREATE OR ALTER PROCEDURE GetFreeSeats(@TrainID INT, @DepartDateTime DATETIME, @CarrTypeID NVARCHAR(200), @StartStatID INT, @EndStatID INT)
AS
BEGIN
	DECLARE @RouteID INT;
	SET @RouteID = (SELECT R.ID FROM [Route] AS R WHERE R.ID = (SELECT T.RouteID FROM Train AS T WHERE T.ID = @TrainID));

	IF OBJECT_ID('temp..#Tickets') IS NOT NULL
	BEGIN
		DROP TABLE #Tickets;
	END;
	CREATE TABLE #Tickets
	(
		ID INT,
		StartStatOnRouteID INT,
		EndStatOnRouteID INT,
		ReservationSeatID INT
	);

	INSERT INTO #Tickets(ID, StartStatOnRouteID, EndStatOnRouteID, ReservationSeatID)
	(SELECT T.ID, T.StationOnRouteStartID, T.StationOnRouteEndID, T.ReservationSeatID
			FROM Ticket AS T
		    WHERE (T.ReservationSeatID IN 
				  (SELECT RS.ID FROM ReservationSeat AS RS WHERE RS.CarriageID IN 
					  (SELECT C.ID FROM Carriage AS C WHERE C.TrainID = @TrainID AND C.CarriageTypeID = @CarrTypeID)))
				  AND CONVERT(DATE, T.DepartureDateTime) = CONVERT(DATE, @DepartDateTime));

	IF OBJECT_ID('temp..#CarrId') IS NOT NULL
	BEGIN
		DROP TABLE #CarrId;
	END;
	CREATE TABLE #CarrId
	(
		ID INT,
		[Order] INT
	);

	INSERT INTO #CarrId(ID, [Order])
	(SELECT C.ID, C.[Order] FROM Carriage AS C WHERE C.TrainID = @TrainID AND C.CarriageTypeID = @CarrTypeID)

	DECLARE @CountOfSeats INT;
	SET @CountOfSeats = (SELECT CT.CountOfSeats FROM CarriageType AS CT WHERE CT.ID = @CarrTypeID);

	IF OBJECT_ID('temp..#FreeSeatsInCarr') IS NOT NULL
	BEGIN
		DROP TABLE #FreeSeatsInCarr;
	END;
	CREATE TABLE #FreeSeatsInCarr
	(
		Number INT,
		CarrOrder INT
	);

	WHILE (SELECT COUNT(#CarrId.ID) FROM #CarrId) > 0
	BEGIN
		DECLARE @SeatNumber INT;
		SET @SeatNumber = @CountOfSeats;
		DECLARE @CarrId INT;
		DECLARE @CarrOrder INT;
		SET @CarrId = (SELECT TOP 1 ID FROM #CarrId);
		SET @CarrOrder = (SELECT C.[Order] FROM Carriage AS C WHERE C.ID = @CarrId);
		WHILE @SeatNumber > 0
			BEGIN
				IF NOT EXISTS 
				(
					SELECT #Tickets.ReservationSeatID FROM #Tickets
						   INNER JOIN ReservationSeat AS RS ON RS.ID = #Tickets.ReservationSeatID
						   WHERE RS.Number = @SeatNumber 
								 AND RS.CarriageID = @CarrId
				)
				BEGIN
					INSERT INTO #FreeSeatsInCarr(Number, CarrOrder)
					(SELECT @SeatNumber, @CarrOrder);
				END;
				IF EXISTS 
				(
					SELECT #Tickets.ReservationSeatID FROM #Tickets
						   INNER JOIN ReservationSeat AS RS ON RS.ID = #Tickets.ReservationSeatID
						   WHERE RS.Number = @SeatNumber
								 AND RS.CarriageID = @CarrId
								 AND RS.ID NOT IN 
									 (SELECT #Tickets.ReservationSeatID FROM #Tickets WHERE 
										 ((SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.ID = #Tickets.StartStatOnRouteID) <= 
										 (SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.RouteID = @RouteID AND SOR1.StationID = @StartStatID) AND 
										 (SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.RouteID = @RouteID AND SOR1.StationID = @StartStatID) < 
										 (SELECT SOR2.[Order] FROM StationOnRoute AS SOR2 WHERE SOR2.ID = #Tickets.EndStatOnRouteID))
										 OR
										 ((SELECT SOR2.[Order] FROM StationOnRoute AS SOR2 WHERE SOR2.ID = #Tickets.EndStatOnRouteID) >=
										 (SELECT SOR2.[Order] FROM StationOnRoute AS SOR2 WHERE SOR2.RouteID = @RouteID AND SOR2.StationID = @EndStatID) AND
										 (SELECT SOR2.[Order] FROM StationOnRoute AS SOR2 WHERE SOR2.RouteID = @RouteID AND SOR2.StationID = @EndStatID) >
										 (SELECT SOR1.[Order] FROM StationOnRoute AS SOR1 WHERE SOR1.ID = #Tickets.StartStatOnRouteID)))
				)
				BEGIN
					INSERT INTO #FreeSeatsInCarr(Number, CarrOrder)
					(SELECT @SeatNumber, @CarrOrder);
				END;
				SET @SeatNumber = @SeatNumber - 1;
			END;
		DELETE FROM #CarrId WHERE ID = @CarrId;
	END;

	SELECT * FROM #FreeSeatsInCarr;
END;

EXEC GetFreeSeats @TrainID = 1, @DepartDateTime = '2020-02-24 01:10:00', @CarrTypeID = 1, 
	 @StartStatID = 1, @EndStatID = 2; 


