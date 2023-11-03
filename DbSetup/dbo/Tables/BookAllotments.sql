CREATE TABLE [dbo].[BookAllotments] (
    [AuthorId]       UNIQUEIDENTIFIER NOT NULL,
    [BookId]         UNIQUEIDENTIFIER NOT NULL,
    [CustomerId]     UNIQUEIDENTIFIER NOT NULL,
    [AllotmentId]    UNIQUEIDENTIFIER CONSTRAINT [DF_BookAllotments_AllotmentId] DEFAULT (newid()) NOT NULL,
    [AllotmentDate]  DATETIME         NOT NULL,
    [ReturnDate]     DATETIME         NOT NULL,
    [BookReturnedOn] DATETIME         NULL,
    CONSTRAINT [PK_BookAllotments] PRIMARY KEY CLUSTERED ([AuthorId] ASC, [BookId] ASC, [CustomerId] ASC, [AllotmentId] ASC),
    CONSTRAINT [FK_BookAllotments_Books] FOREIGN KEY ([AuthorId], [BookId]) REFERENCES [dbo].[Books] ([AuthorId], [BookId]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_BookAllotments_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId]) ON DELETE CASCADE ON UPDATE CASCADE
);

