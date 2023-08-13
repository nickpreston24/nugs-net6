CREATE TABLE Part ([ID] [int] IDENTITY, [Name] [varchar](128)
GO
CREATE PROC InsertPart ([@Name] [varchar](128))
AS
	INSERT INTO Part (Name)
		OUTPUT Inserted.ID
		VALUES (@Name)

GO