CREATE TABLE [dbo].[Books] (
    [AuthorId]        UNIQUEIDENTIFIER NOT NULL,
    [BookId]          UNIQUEIDENTIFIER CONSTRAINT [DF_Books_BookId] DEFAULT (newid()) NOT NULL,
    [Title]           VARCHAR (100)    NOT NULL,
    [Publication]     VARCHAR (100)    NOT NULL,
    [PublicationYear] SMALLINT         NOT NULL,
    [ISBN]            VARCHAR (25)     NOT NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([AuthorId] ASC, [BookId] ASC),
    CONSTRAINT [FK_Books_Authors] FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[Authors] ([AuthorId]) ON DELETE CASCADE ON UPDATE CASCADE
);

