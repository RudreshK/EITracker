ALTER TABLE [dbo].[LeaveRequestDetail]
	ADD CONSTRAINT [PK_LeaveRequestDetails]
	PRIMARY KEY CLUSTERED
	(
		[LeaveId], [LeaveRequestDetailId] ASC
	)