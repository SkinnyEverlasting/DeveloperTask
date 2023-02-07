USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Get_Authors_By_Name]    Script Date: 2/7/2023 9:41:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Get_Authors_By_Name]
	@name nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT * FROM Authors
	WHERE FirstName LIKE '%' + @name + '%'
END
