﻿CREATE PROCEDURE GetBookList
@authorId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT BookId, Title
		FROM Books
		WHERE AuthorId = @authorId
END