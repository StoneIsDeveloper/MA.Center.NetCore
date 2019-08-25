/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc dbo.GetAllUserInfos

AS
BEGIN
  select info.Id as Id
          ,u.Id as IdentityUserId
		  ,info.NickName as NickName
		  ,u.Email as Email
		  ,u.UserName as UserName
   from 
  
   [dbo].[AspNetUsers] u (nolock) 
    join  dbo.UserInfos info (nolock)
	 on info.IdentityUserId = u.Id
END