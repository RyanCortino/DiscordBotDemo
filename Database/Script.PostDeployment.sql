IF NOT EXISTS (select 1 from dbo.[User])
BEGIN
	INSERT INTO dbo.[User] (AvatarId)
	VALUES ('203880205507493888');
END