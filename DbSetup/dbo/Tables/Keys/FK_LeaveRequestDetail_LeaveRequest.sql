ALTER TABLE [dbo].[LeaveRequestDetail]
	ADD CONSTRAINT [FK_LeaveRequestDetail_LeaveRequest]
	FOREIGN KEY ([LeaveId])
	REFERENCES [dbo].[LeaveRequest] ([LeaveId])
	ON DELETE CASCADE
	ON UPDATE NO ACTION