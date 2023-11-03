CREATE TABLE [dbo].[Authors] (
    [AuthorId]      UNIQUEIDENTIFIER CONSTRAINT [DF_Authors_AuthorId] DEFAULT (newid()) NOT NULL,
    [Name]          VARCHAR (50)     NOT NULL,
    [Address]       VARCHAR (200)    NULL,
    [Qualification] VARCHAR (50)     NULL,
    CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED ([AuthorId] ASC)
);

