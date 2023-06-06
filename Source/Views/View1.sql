create or Alter view connections
as
select uc.ConnectedUser_ID from UserConnections uc join UserInformation ui ON uc.User_ID = ui.User_ID
where ui.User_ID = 7
--join User_Post up on up.User_Id = uc.ConnectedUser_ID
go
select * from connections;