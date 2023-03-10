USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Add_Book_Information]    Script Date: 2/7/2023 9:40:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Add_Book_Information] 
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
		
		--insert new book
		INSERT INTO [dbo].[Books]
			   ([Title]
			   ,[Description]
			   ,[ImagePath]
			   ,[Rating]
			   ,[ReleaseDate]
			   ,[TakenAway])
		 VALUES
			   (@title, @description, @imagePath, @rating, @releaseDate, 0)

		DECLARE @bookId int;

		SET @bookId = SCOPE_IDENTITY();

		--declaring variables for storing author information
		DECLARE @AuthorFirstName nvarchar(100);
		DECLARE @AuthorLastName nvarchar(100);
		DECLARE @AuthorBirthDate datetime;

		DECLARE @authorsCount int;
		Select @authorsCount = Count(*) From @authorsList

		--iterating over received authors list to find out which authors already exists in database
		WHILE (@authorsCount > 0)
		BEGIN
			SELECT  @AuthorFirstName = FirstName,
					@AuthorLastName = LastName,
					@AuthorBirthDate = BirthDate
			From @authorsList
			WHERE ID = @authorsCount;

			--if author already exists, just inserting existing author id and book id in AuthorsAndBooks table, else i'm inserting new author 
			IF EXISTS(SELECT * FROM Authors WHERE FirstName = @AuthorFirstName AND LastName = @AuthorLastName AND BirthDate = @AuthorBirthDate)
			BEGIN
				DECLARE @ExistingAuthorId int;
				SELECT @ExistingAuthorId = ID FROM Authors WHERE FirstName = @AuthorFirstName AND LastName = @AuthorLastName AND BirthDate = @AuthorBirthDate
				INSERT INTO [dbo].[AuthorsAndBooks]
					   ([AuthorID]
					   ,[BookID])
				VALUES (@ExistingAuthorId, @bookId)
			END
			ELSE
			BEGIN
				INSERT INTO [dbo].[Authors]
					   ([FirstName]
					   ,[LastName]
					   ,[BirthDate])
				VALUES (@AuthorFirstName, @AuthorLastName, @AuthorBirthDate)

				DECLARE @insertedAuthorID int;

				SET @insertedAuthorID = SCOPE_IDENTITY();

				INSERT INTO [dbo].[AuthorsAndBooks]
						   ([AuthorID]
						   ,[BookID])
				VALUES (@insertedAuthorID, @bookId) 

			END
			SET @authorsCount = @authorsCount - 1;

		END

		COMMIT TRANSACTION
		RETURN 0;
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1;
	END CATCH

END





