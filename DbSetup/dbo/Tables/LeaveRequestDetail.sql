CREATE TABLE [dbo].[LeaveRequestDetail]
(
	[LeaveId] UNIQUEIDENTIFIER NOT NULL, 
    [LeaveRequestDetailId] UNIQUEIDENTIFIER NOT NULL, 
    [AssignedTo] UNIQUEIDENTIFIER NOT NULL, 
    [CC] NVARCHAR(1024) NULL, 
    [DisplayOrder] TINYINT NOT NULL, 
    [Comments] NVARCHAR(512) NULL, 
    [Status] TINYINT NOT NULL,
    [CreatedTime] DATETIME2 NOT NULL, 
    [CreatedById] UNIQUEIDENTIFIER NOT NULL, 
    [ModifiedTime] DATETIME2 NOT NULL, 
    [ModifiedById] UNIQUEIDENTIFIER NOT NULL, 
    [ConcurrencyStamp] TIMESTAMP NOT NULL
)
