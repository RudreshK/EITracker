CREATE TABLE [dbo].[Customers] (
    [CustomerId]    UNIQUEIDENTIFIER CONSTRAINT [DF_Customers_CustomerId] DEFAULT (newid()) NOT NULL,
    [CustomerName]  VARCHAR (100)    NOT NULL,
    [ContactNumber] VARCHAR (20)     NOT NULL,
    [Address]       VARCHAR (200)    NOT NULL,
    [Email]         VARCHAR (100)    NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

