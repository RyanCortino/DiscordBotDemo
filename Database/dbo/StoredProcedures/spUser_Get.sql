CREATE PROCEDURE [dbo].[spUser_Get]
	@Id int
AS
BEGIN
	SELECT Id, AvatarId
	FROM dbo.[User]
	WHERE Id = @Id;
END