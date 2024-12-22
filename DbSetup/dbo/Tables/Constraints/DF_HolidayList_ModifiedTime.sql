ALTER TABLE [dbo].[HolidayList]
	ADD CONSTRAINT [DF_HolidayList_ModifiedTime]
	DEFAULT (SYSUTCDATETIME()) FOR [ModifiedTime];