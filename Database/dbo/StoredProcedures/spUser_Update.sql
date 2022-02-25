CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int,
	@AvatarId nvarchar(50)
AS
BEGIN
	UPDATE dbo.[User]
	SET AvatarId = @AvatarId
	WHERE Id = @Id
END
