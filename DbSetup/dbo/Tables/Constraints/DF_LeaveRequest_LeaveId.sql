ALTER TABLE [dbo].[LeaveRequest]
	ADD CONSTRAINT [DF_LeaveRequest_LeaveId]
	DEFAULT NEWID()
	FOR [LeaveId]