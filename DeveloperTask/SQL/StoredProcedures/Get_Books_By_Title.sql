USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Get_Books_By_Title]    Script Date: 2/7/2023 9:42:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Get_Books_By_Title]
	@title nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Books
	WHERE Title LIKE '%' + @title + '%'
END

