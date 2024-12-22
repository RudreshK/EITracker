ALTER TABLE [dbo].[HolidayList]
	ADD CONSTRAINT [DF_HolidayList_HolidayId]
	DEFAULT NEWID()
	FOR [HolidayId]
