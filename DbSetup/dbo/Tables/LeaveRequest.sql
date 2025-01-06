CREATE TABLE [dbo].[LeaveRequest]
(	
    [LeaveId] UNIQUEIDENTIFIER NOT NULL, 
    [EmployeeId] UNIQUEIDENTIFIER NOT NULL , 
    [NoOfDays] decimal(4,2) NOT NULL,    
    [StartDate] DATETIME NOT NULL, 
    [EndDate] DATETIME NOT NULL, 
    [LeaveType] TINYINT NOT NULL,
    [Status] TINYINT NOT NULL, 
    [Subject] dbo.shorttext NOT NULL,
    [Message] dbo.longtext NOT NULL,  
    [Attachments] NVARCHAR(MAX) NULL,  
    [CreatedTime] DATETIME2 NOT NULL, 
    [CreatedById] UNIQUEIDENTIFIER NOT NULL, 
    [ModifiedTime] DATETIME2 NOT NULL, 
    [ModifiedById] UNIQUEIDENTIFIER NOT NULL, 
    [ConcurrencyStamp] TIMESTAMP NOT NULL
)
