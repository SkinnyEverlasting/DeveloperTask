USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Return_Book]    Script Date: 2/7/2023 9:42:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Return_Book]
	@bookId int
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS(SELECT * FROM Books WHERE ID = @bookId AND TakenAway = 1)
	BEGIN
		UPDATE Books SET TakenAway = 0 WHERE ID = @bookId
		RETURN 0;
	END
	ELSE
	BEGIN
		RETURN -1;
	END
END
