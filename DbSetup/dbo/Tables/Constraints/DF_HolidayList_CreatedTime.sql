ALTER TABLE [dbo].[HolidayList]
	ADD CONSTRAINT [DF_HolidayList_CreatedTime]
	DEFAULT (SYSUTCDATETIME()) FOR [CreatedTime];