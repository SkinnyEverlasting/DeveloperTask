USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Books_Information]    Script Date: 2/7/2023 9:41:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Get_All_Books_Information]
AS
BEGIN
	SET NOCOUNT ON;

	--creating temporary table to store result
	CREATE TABLE #ResultTable(BookID int, Title nvarchar(100), Description nvarchar(200), ImagePath nvarchar(200), Authors nvarchar(200))

	--getting all authors and books relation in temporary table
	SELECT * INTO
	#TempAuthorsAndBooks
	FROM AuthorsAndBooks

	Declare @selectedBookId int
	Declare @selectedAuthorId int

	WHILE ((Select Count(BookID) From #TempAuthorsAndBooks) > 0)
	BEGIN
		--retrieving bookId and authorId
		Select Top 1 @selectedBookId = BookID, @selectedAuthorId = AuthorID From #TempAuthorsAndBooks

		--if book does not exist in result table, inserting book data with 1 author in temporary table
		IF NOT EXISTS(SELECT * FROM #ResultTable WHERE BookID = @selectedBookId)
		BEGIN
				
			INSERT INTO #ResultTable (BookID, Title, Description, ImagePath, Authors)
			SELECT books.ID, 
					books.Title,
					books.Description, 
					books.ImagePath,
					(authors.FirstName + ' ' + authors.LastName) AS Author
			FROM Books books
			JOIN Authors authors ON authors.ID = @selectedAuthorId
			WHERE books.ID = @selectedBookId
		END
		ELSE
		BEGIN
			--if book record already exists in result table (with info and authors), i'm just adding another author to already existed book record
			Declare @authorName nvarchar(50);
			SELECT @authorName = (FirstName + ' ' + LastName) FROM Authors WHERE ID = @selectedAuthorId
				
			UPDATE #ResultTable 
			SET Authors = Authors + ', ' +  @authorName
			WHERE BookID = @selectedBookId
		END
			--deleting already iterated author and book from temporary table
			Delete #TempAuthorsAndBooks Where AuthorID = @selectedAuthorId AND BookID = @selectedBookId
		END


		SELECT * FROM #ResultTable

END

