ALTER TABLE [dbo].[LeaveRequestDetail]
	ADD CONSTRAINT [DF_LeaveRequestDetail_LeaveRequestDetailId]
	DEFAULT NEWID()
	FOR [LeaveRequestDetailId]