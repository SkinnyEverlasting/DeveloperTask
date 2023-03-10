USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Check_If_Book_Exists]    Script Date: 2/7/2023 9:40:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Check_If_Book_Exists]
	@bookId int
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT * FROM Books WHERE ID = @bookId)
	BEGIN
		RETURN 0;
	END

	RETURN -1;
END

