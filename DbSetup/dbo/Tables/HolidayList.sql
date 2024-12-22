CREATE TABLE [dbo].[HolidayList]
(
	[HolidayId] UNIQUEIDENTIFIER NOT NULL , 
    [HolidayDate] DATETIME2 NOT NULL, 
    [HolidayName] NVARCHAR(100) NOT NULL, 
    [WeeKDay] TINYINT NOT NULL, 
    [CreatedById] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedTime] DATETIME2 NOT NULL, 
    [ModifiedById] UNIQUEIDENTIFIER NOT NULL, 
    [ModifiedTime] DATETIME2 NOT NULL,
    [ConcurrencyStamp] TIMESTAMP NOT NULL
)
