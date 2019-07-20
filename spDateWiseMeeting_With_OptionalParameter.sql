USE [MeetingManagementDb]
GO

/****** Object:  StoredProcedure [dbo].[spDateWiseMeeting]    Script Date: 04/25/19 1:48:37 PM ******/
DROP PROCEDURE [dbo].[spDateWiseMeeting]
GO

/****** Object:  StoredProcedure [dbo].[spDateWiseMeeting]    Script Date: 04/25/19 1:48:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spDateWiseMeeting]
(
	@startDate date,
	@endDate date,
	@venueId int = null,
	@employeeId int = null
)
AS 

BEGIN

SELECT
		mt.MeetingId,
		mt.MeetingName, 
		mt.BeginningTime,
		mt.EndTime,
		mt.EmployeeId,
		emp.EmployeeName,
		mt.VenueId,
		vn.VenueName
FROM Meetings mt
JOIN Employees emp ON mt.EmployeeId = emp.EmployeeId
JOIN Venues vn ON mt.VenueId = vn.VenueId

WHERE		mt.BeginningTime>=@startDate AND mt.EndTime<=@endDate
				AND (mt.VenueId = @venueId or @venueId is null )AND( mt.EmployeeId = @employeeId or @employeeId is null)

END
GO
declare @startDate datetime
declare @endDate datetime 

set @startDate = '2018-12-13'   -- 6th of April, 2012
set @endDate = '2018-12-31'   -- 6th of August, 2012

exec spDateWiseMeeting  @startDate, @endDate

