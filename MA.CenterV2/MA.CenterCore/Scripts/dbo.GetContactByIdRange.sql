CREATE PROCEDURE [dbo].[GetContactByIdRange]
	@IDList AS  dbo.ListIDTableType  readonly
	
AS
	 SELECT [ContactId]
      ,[OwnerID]
      ,[Name]
      ,[Address]
      ,[City]
      ,[State]
      ,[Zip]
      ,[Email]
      ,[Status]
	 FROM [dbo].[Contacts] 
	  where ContactId in (  select ID from @IDList )
RETURN 