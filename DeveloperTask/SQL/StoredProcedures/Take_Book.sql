USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Take_Book]    Script Date: 2/7/2023 9:42:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Take_Book]
	@bookId int
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT * FROM Books WHERE ID = @bookId)
	BEGIN

		DECLARE @bookStatus bit;
		SELECT @bookStatus = TakenAway FROM Books WHERE ID = @bookId

		IF(@bookStatus = 1)
		BEGIN	
			RETURN 1;
		END

		UPDATE Books SET TakenAway = 1 WHERE ID = @bookId
		RETURN 0;
	END
	ELSE
	BEGIN
		RETURN -1;
	END
END
