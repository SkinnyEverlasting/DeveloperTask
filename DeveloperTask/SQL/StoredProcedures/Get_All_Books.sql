USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Books]    Script Date: 2/7/2023 9:41:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Get_All_Books]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Books;
END

