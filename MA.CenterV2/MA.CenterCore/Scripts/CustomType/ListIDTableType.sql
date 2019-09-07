CREATE TYPE dbo.ListIDTableType
AS TABLE
(
  ID INT
);
GO

 --Exec sp_addtype contactIdList, int, 'Not Null'

 -- Exec sp_droptype 'ListIDTableType'
