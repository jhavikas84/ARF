
--delete bookings
--delete flights
--delete passengerbookings
--delete passengers 

INSERT INTO [dbo].[Flights] ([FlightNumber], [FlightName], [Description], [StartTime], [EndTime], 
							 [SeatCapacity], [DepartureCity], [ArrivalCity], [BasePrice], [TaxPercentage], 
							 [DiscountRate]) 
VALUES ('ARF-101', 'ARF-Flight-Mel-Syd', 'Flight from Melbourne to Sydney', 
'10:30:00', '11:30:00', 50, 'Melbourne', 'Sydney', 55, 5.00, 0)

INSERT INTO [dbo].[Flights] ([FlightNumber], [FlightName], [Description], [StartTime], [EndTime], 
							 [SeatCapacity], [DepartureCity], [ArrivalCity], [BasePrice], [TaxPercentage], 
							 [DiscountRate]) 
VALUES ('ARF-102', 'ARF-Flight-Syd-Mel', 'Flight from Sydney to Melbourne', 
'12:30:00', '13:30:00', 50, 'Sydney', 'Melbourne', 55, 5.00, 0)

INSERT INTO [dbo].[Flights] ([FlightNumber], [FlightName], [Description], [StartTime], [EndTime], 
							 [SeatCapacity], [DepartureCity], [ArrivalCity], [BasePrice], [TaxPercentage], 
							 [DiscountRate]) 
VALUES ('ARF-103', 'ARF-Flight-Mel-Perth', 'Flight from Melbourne to Perth', 
'06:30:00', '09:30:00', 55, 'Melbourne', 'Sydney', 55, 9.00, 1.05)

INSERT INTO [dbo].[Flights] ([FlightNumber], [FlightName], [Description], [StartTime], [EndTime], 
							 [SeatCapacity], [DepartureCity], [ArrivalCity], [BasePrice], [TaxPercentage], 
							 [DiscountRate]) 
VALUES ('ARF-104', 'ARF-Flight-Perth-Mel', 'Flight from Perth to Melbourne', 
'08:15:00', '11:00:00', 55, 'Melbourne', 'Sydney', 55, 9.75, 1.00)

Select * 
from Flights