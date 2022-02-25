CREATE PROCEDURE [dbo].[spUser_Insert]
	@AvatarId nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.[User] (AvatarId)
	VALUES (@AvatarId);
END