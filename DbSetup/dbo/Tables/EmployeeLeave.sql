CREATE TABLE [dbo].[EmployeeLeave]
(
	[EmployeeId] UNIQUEIDENTIFIER NOT NULL , 
    [LeaveId] UNIQUEIDENTIFIER NOT NULL DEFAULT (newid()), 
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [LeaveType] NCHAR(10) NOT NULL, 
    [Status] NCHAR(10) NOT NULL, 
    [Comments] NVARCHAR(50) NULL,
    [CreatedTime] DATETIME2 NOT NULL, 
    [CreatedById] UNIQUEIDENTIFIER NOT NULL, 
    [ModifiedTime] DATETIME2 NOT NULL, 
    [ModifiedById] UNIQUEIDENTIFIER NOT NULL, 
    [ConcurrencyStamp] TIMESTAMP NOT NULL, 
    PRIMARY KEY ([LeaveId]), 
    CONSTRAINT [FK_EmployeeLeave_AspNetUsers] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers]([Id]),
)
