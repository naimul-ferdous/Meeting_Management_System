USE [MeetingManagementDb]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetMeetingMemberWithEmployeeName]    Script Date: 28-Jan-19 12:50:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[sp_GetMeetingMemberWithEmployeeName]
@EmployeeId int
as
Begin
Select m.MeetingMemberId,m.MeetingId,em.EmployeeId,em.EmployeeName from MeetingMembers m join Employees em on m.EmployeeId=em.EmployeeId where em.EmployeeId=@EmployeeId;
End
GO

exec sp_GetMeetingMemberWithEmployeeName 72;

