CREATE TABLE [dbo].[Product]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProductName] NCHAR(10) NOT NULL, 
    [Brand] NCHAR(10) NULL, 
    [Price] NCHAR(10) NULL
)
