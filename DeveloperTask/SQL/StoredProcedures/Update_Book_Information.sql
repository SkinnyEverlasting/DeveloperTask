USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Update_Book_Information]    Script Date: 2/7/2023 9:42:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Update_Book_Information]
	@bookId int,
	@title nvarchar(100),
	@description nvarchar(200),
	@imagePath nvarchar(200),
	@rating money,
	@releaseDate datetime,
	@authorsList UT_Authors readonly
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		--updating book
		UPDATE [dbo].[Books]
		SET [Title] = @title,
			[Description] = @description,
			[ImagePath] = @imagePath,
			[Rating] = @rating,
			[ReleaseDate] = @releaseDate,
			[TakenAway] = 0
		WHERE ID = @bookId

		--getting all old authors for book
		CREATE TABLE #OldAuthorIds(AuthId int);

		INSERT INTO #OldAuthorIds
		SELECT AuthorID FROM AuthorsAndBooks 
		WHERE BookID = @bookId

		--deleting author and book relation data from table
		DELETE FROM AuthorsAndBooks WHERE BookID = @bookId

		--if oldAuthor does not exist in AuthorsAndBooks relation table, then deleting author, because no book is related to him/her
		Declare @OldAuthorId int
		WHILE ((Select Count(*) From #OldAuthorIds) > 0)
		BEGIN
			Select Top 1 @OldAuthorId = AuthId From #OldAuthorIds

			IF NOT EXISTS(SELECT * FROM AuthorsAndBooks WHERE AuthorID = @OldAuthorId)
			BEGIN
				DELETE FROM Authors WHERE ID = @OldAuthorId
			END

			Delete #OldAuthorIds Where AuthId = @OldAuthorId
		END

		--getting count of authors list
		DECLARE @authorsCount int;

		SELECT @authorsCount = COUNT(FirstName) FROM @authorsList;

		--inserting new list of authors
		INSERT INTO [dbo].[Authors]
			   ([FirstName]
			   ,[LastName]
			   ,[BirthDate])
		SELECT FirstName, LastName, BirthDate FROM @authorsList

		--retrieving new inserted authorIds
		SELECT TOP(@authorsCount) ID 
		INTO #AuthorIds 
		FROM Authors ORDER BY ID DESC

		--inserting authorIds and bookId relation in table
		INSERT INTO [dbo].[AuthorsAndBooks]
			   ([AuthorID]
			   ,[BookID])
		 SELECT *, @bookId FROM #AuthorIds
    
		COMMIT TRANSACTION;
		RETURN 0;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION;
		RETURN -1;
	END CATCH
END
