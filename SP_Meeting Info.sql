USE [MeetingManagementDb]
GO
/****** Object:  StoredProcedure [dbo].[sprMeetingInfo]    Script Date: 18/03/2019 11:33:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sprMeetingInfo]

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

END





GO
