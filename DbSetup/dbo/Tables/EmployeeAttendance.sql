CREATE TABLE [dbo].[EmployeeAttendance]
(
	[EmployeeId] UNIQUEIDENTIFIER NOT NULL , 
    [AttendanceId] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [Date] DATE NOT NULL, 
    [CheckIn] DATETIME NOT NULL, 
    [CheckOut] DATETIME NOT NULL, 
    [Status] NCHAR(10) NOT NULL, 
    [CreatedTime] DATETIME2 NOT NULL, 
    [CreatedById] UNIQUEIDENTIFIER NOT NULL, 
    [ModifiedTime] DATETIME2 NOT NULL, 
    [ModifiedById] UNIQUEIDENTIFIER NOT NULL, 
    [ConcurrencyStamp] TIMESTAMP NOT NULL, 
    PRIMARY KEY ([AttendanceId]), 
    CONSTRAINT [FK_EmployeeAttendance_AspNetUsers] FOREIGN KEY (EmployeeId) REFERENCES [AspNetUsers]([Id])
)
