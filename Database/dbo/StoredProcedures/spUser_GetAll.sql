CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
BEGIN
	SELECT Id, AvatarId
	FROM dbo.[User];
END