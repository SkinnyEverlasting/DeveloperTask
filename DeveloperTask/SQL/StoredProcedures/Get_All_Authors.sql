USE [DeveloperTask]
GO
/****** Object:  StoredProcedure [dbo].[Get_All_Authors]    Script Date: 2/7/2023 9:41:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Get_All_Authors]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT DISTINCT * FROM Authors
END

