drop DATABASE IT_Support_Tickect_System
CREATE DATABASE IT_Support_Tickect_System
USE IT_Support_Tickect_System

--Drop table Ticket_History
--Drop table Ticket
--Drop table Priority_


create table Department(
Department_No int not null,
Department_Name varchar(20)

CONSTRAINT PK_Department_No PRIMARY KEY (Department_No)
);

insert into Department values 
(1, 'IT'),
(2, 'Accounting'),
(3, 'Finance'),
(4, 'Customer Survice')


create table Priority_(
Priority_No int not null,
Priority_Desc varchar(40)

CONSTRAINT PK_Priority_No PRIMARY KEY (Priority_No)
);

insert into Priority_ values
(1, 'Very High Priority'),
(2, 'High Priority'),
(3, 'Mid Priority'),
(4, 'Low Priority')


create table Login_(
Log_ID int not null,
User_Name_ varchar(60) not null,
Password_ varchar(40) not null

CONSTRAINT PK_Log_ID PRIMARY KEY (Log_ID)
);

insert into Login_ values
--Deafult
(100, 'Everyone', 'do not use this acc'),
--Clients
(101, 'Mark_Baxton', 'MB10001'),
(102, 'Jack_West','JW10002'),
(103, 'Kaitty_Sanders','KS10003'),
(104, 'Pet_McCluskey','PM10004'),
--Contrater
(105, 'Tiffany_Norris', 'TN10005'),
(106, 'Zackary_Peck', 'ZP10006'),
(107, 'Tobi_Knights','TK10007'),
(108, 'Emily_Lane','EL10008')


create table Client(
Client_ID int not null,
First_Name varchar(30) not null,
Last_Name varchar(30) not null,
Department_No int not null,
Phone varchar(11) not null,
Email varchar(60) not null,
Log_ID int not null

CONSTRAINT PK_Client_ID PRIMARY KEY (Client_ID)
CONSTRAINT FK_Department_No FOREIGN KEY (Department_No) REFERENCES Department(Department_No),
CONSTRAINT FK_Log_ID FOREIGN KEY (Log_ID) REFERENCES Login_(Log_ID) on  Delete Cascade
);

Insert into Client values 
(10001, 'Mark', 'Baxton', 2, '02835943265', 'mbaxton@gmail.co.uk', 101),
(10002, 'Jack', 'West', 3, '02845736194', 'jwest@gmail.co.uk', 102),
(10003, 'Kaitty', 'Sanders', 4, '02867314953', 'k_sanders@gmail.co.uk', 103),
(10004, 'Pet', 'McCluskey', 4, '02845638479', 'pet_mc@gmail.co.uk', 104),
(10005, 'Tiffany', 'Norris', 1, '02816544277', 'tiffany_n@gmail.co.uk', 105),
(10006, 'Zackary', 'Peck', 1, '02845536874', 'zacky_boi@gmail.co.uk', 106),
(10007, 'Tobi', 'Knights', 4, '02895987226', 'knights_t@gmail.co.uk', 107),
(10008, 'Emily', 'Lane', 1, '02845653885', 'emily_l@gmail.co.uk', 108)

create table Contractor(
Contractor_ID int not null,
First_Name varchar(30) not null,
Last_Name varchar(30) not null,
Department_No int not null,
Phone varchar(11) not null,
Email varchar(60) not null,
Log_ID int not null,
Client_ID int not null

CONSTRAINT PK_Contractor_ID PRIMARY KEY (Contractor_ID)
CONSTRAINT FK_Department_No_2 FOREIGN KEY (Department_No) REFERENCES Department(Department_No),
CONSTRAINT FK_Log_ID_2 FOREIGN KEY (Log_ID) REFERENCES Login_(Log_ID),
CONSTRAINT FK_Client_ID_2 FOREIGN KEY (Client_ID) REFERENCES Client(Client_ID) on Delete Cascade
);


insert into Contractor values
(10000, 'Deafult', 'All Contractors', 1, '02865412322', 'everyone@gmail.co.uk', 100, 10001),
(10001, 'Tiffany', 'Norris', 1, '02816544277', 'tiffany_n@gmail.co.uk', 105, 10005),
(10002, 'Zackary', 'Peck', 1, '02845536874', 'zacky_boi@gmail.co.uk', 106, 10006),
(10003, 'Tobi', 'Knights', 4, '02895987226', 'knights_t@gmail.co.uk', 107, 10007),
(10004, 'Emily', 'Lane', 1, '02845653885', 'emily_l@gmail.co.uk', 108, 10008)



create table Ticket(
Problem_No int not null,
Client_ID int not null,
Date_time_Raised datetime not null,
Priority_No int not null,
Subject_Title varchar(60) not null,
Description_ varchar(Max) not null,
Completed tinyint not null,
Date_time_completed datetime null,
Contractor_ID int not null

CONSTRAINT PK_Problem_No PRIMARY KEY (Problem_No)
CONSTRAINT FK_Client_ID FOREIGN KEY (Client_ID) REFERENCES Client(Client_ID),
CONSTRAINT FK_Priority_No FOREIGN KEY (Priority_No) REFERENCES Priority_(Priority_No),
CONSTRAINT FK_Contractor_ID FOREIGN KEY (Contractor_ID) REFERENCES Contractor(Contractor_ID)
);

insert into Ticket values 
(10001, 10002, '2020-06-19 14:54:00', 3, 'Computer is slower for some reason', 'I am having difficalty doing daily taks as my computer has slowed down.', 0, null, 10003),
(10002, 10004, '2020-07-22 16:32:00', 1, 'Phones are not working', 'Seemingly, all phone communications are not working', 1, '2020-07-22 20:41:00', 10002),
(10003, 10003, '2020-08-22 10:05:00', 2, 'I cannot login', 'When I try to logon the message "You currently do not have access the this file." appears', 0, null, 10002)



create table Ticket_History(
Update_Ref_No int not null,
Problem_No int not null,
Client_ID int not null,
Date_time_Raised datetime not null,
Priority_No int not null,
Subject_Title varchar(60) not null,
Description_ varchar(Max) not null,
Completed tinyint not null,
Date_time_completed datetime null,
Contractor_ID int not null,
Process text not null,
Date_Time datetime not null,

CONSTRAINT PK_Update_Ref_No PRIMARY KEY (Update_Ref_No),
CONSTRAINT FK_Client_ID_3 FOREIGN KEY (Client_ID) REFERENCES Client(Client_ID),
CONSTRAINT FK_Priority_No_2 FOREIGN KEY (Priority_No) REFERENCES Priority_(Priority_No),
CONSTRAINT FK_Contractor_ID_2 FOREIGN KEY (Contractor_ID) REFERENCES Contractor(Contractor_ID)
);

insert into Ticket_History values 
--(1000, 10001, 10002, '2020-06-19 14:54:00', 3, 'Computer is slower for some reason', 'I am having difficalty doing daily taks as my computer has slowed down.', 0, null, 10003, 'Added','2020-06-19 14:54:00'),
(1001, 10002, 10004, '2020-07-22 16:32:00', 1, 'Phones are not working', 'Seemingly, all phone communications are not working', 1, '2020-07-22 20:41:00', 10002, 'Added', '2020-07-22 16:32:00'),
(1002, 10003, 10003, '2020-08-22 10:05:00', 2, 'I cannot login', 'When I try to logon the message "You currently do not have access the this file." appears', 0, null, 10002, 'Added', '2020-08-22 10:05:00')



create table Key_Words(
Word_ID int not null,
Word varchar(30) not null,
Ignore_Word tinyint not null

CONSTRAINT PK_Word_ID PRIMARY KEY (Word_ID),
CONSTRAINT U_Word UNIQUE (Word)
);

insert into Key_Words values
(100, 'PC', 0),
(101, 'Computer', 0),
(102, 'Server', 0),
(104, 'BlueScreen', 0),
(105, 'Beeping', 0),
(106, 'Crash', 0),
(107, 'Crashing', 0),
(108, 'PersonalComputer', 0),
(109, 'Virus', 0),
(110, 'Disconnect', 0),
(111, 'Diconnected', 0),
(112, 'Malware', 0)

create table New_Key_Words(
Word_ID int not null,
Word varchar(30) not null,

CONSTRAINT PK_Word_ID_2 PRIMARY KEY (Word_ID),
CONSTRAINT U_Word_2 UNIQUE (Word)
);

insert into New_Key_Words values
(100, 'Defult')

create table Spcialties(
Spcialties_ID int not null,
Spcialties_Name varchar(30) not null

CONSTRAINT PK_Spcialties_ID PRIMARY KEY (Spcialties_ID),
);

insert into Spcialties values
(100, 'AI Managment'),
(101, 'Server Managment'),
(102, 'Basic IT Skills'),
(103, 'Fierwalls'),
(104, 'Web Development'),
(105, 'Hypervisors')


create table Contractor_Spcialties(
Spcialties_ID int not null,
Contractor_ID int not null

CONSTRAINT PK_Contractor_Spcialty PRIMARY KEY (Spcialties_ID, Contractor_ID),
CONSTRAINT FK_Contractor_ID_3 FOREIGN KEY (Contractor_ID) REFERENCES Contractor(Contractor_ID),
CONSTRAINT FK_Spcialty_ID_2 FOREIGN KEY (Spcialties_ID) REFERENCES Spcialties(Spcialties_ID),
);

insert into Contractor_Spcialties values
(100, 10001),
(100, 10002),
(103, 10001),
(101, 10002),
(102, 10003),
(104, 10004),
(105, 10002),
(101, 10001)


create table Spcialty_Key_Words(
Spcialties_ID int not null,
Word_ID int not null

CONSTRAINT PK_Spcialty_Key_Word PRIMARY KEY (Spcialties_ID, Word_ID),
CONSTRAINT FK_Spcialty_ID_3 FOREIGN KEY (Spcialties_ID) REFERENCES Spcialties(Spcialties_ID),
CONSTRAINT FK_Word_ID_2 FOREIGN KEY (Word_ID) REFERENCES Key_words(Word_ID)
)

insert into Spcialty_Key_Words values
(101, 102),
(101, 110),
(101, 111),
(102, 100),
(102, 101),
(102, 104),
(102, 105),
(102, 106),
(102, 107),
(102, 108),
(102, 109),
(102, 112),
(103, 102),
(103, 109),
(103, 110),
(103, 111),
(103, 112),
(104, 102),
(104, 106),
(104, 107),
(104, 110),
(104, 111),
(105, 102),
(105, 106),
(105, 107),
(105, 110),
(105, 111)




--stored procedures
--Report
--Drop procedure Get_Tickets_Client_Report
create procedure Get_Tickets_Client_Report @uname varchar(60), @pword varchar(20)
as
select t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client, c.Phone, c.Email, t.Date_time_Raised, p.Priority_Desc, t.Subject_Title, t.Description_,
(con.First_Name + ' ' + con.Last_Name) as Contractor, con.Phone as Con_Phone, con.Email as Con_Email, t.Date_time_completed from Ticket as t
join Contractor as con on con.Contractor_ID = t.Contractor_ID
join Client as c on c.Client_ID = t.Client_ID
join Login_ as l on l.Log_ID = c.Log_ID
join Priority_ as p on p.Priority_No = t.Priority_No
where l.User_Name_ = @uname and l.Password_ = @pword

exec Get_Tickets_Client_Report @uname = 'Jack_West', @pword = 'JW10002'


--ticket search
--Drop procedure Find_Ticket
Create procedure Find_Ticket @uname varchar(60), @pword varchar(20), @from datetime, @to datetime
as
select t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client_Name, 
t.Date_time_Raised, p.Priority_Desc, t.Subject_Title, t.Completed, t.Date_time_completed, 
(con.First_Name + ' ' + con.Last_Name) as Contractor_Name from Ticket as t
join Client as c on t.Client_ID = c.Client_ID
join Contractor as con on t.Contractor_ID = con.Contractor_ID
join Priority_ as p on t.Priority_No = p.Priority_No
join Login_ as l on l.Log_ID = c.Log_ID
where l.User_Name_ = @uname and l.Password_ = @pword and t.Date_time_Raised between @from and @to;

exec Find_Ticket @uname = 'Jack_West', @pword = 'JW10002',  @from = '2019/01/01 00:00:00', @to = '2021/01/01 00:00:00';


--ticket history search
--Drop procedure Find_Ticket_History
Create procedure Find_Ticket_History @uname varchar(60), @pword varchar(20), @from datetime, @to datetime
as
select th.Update_Ref_No as Update_No, t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client_Name, 
t.Date_time_Raised, p.Priority_Desc,  th.Process, t.Subject_Title, t.Completed, t.Date_time_completed, 
(con.First_Name + ' ' + con.Last_Name) as Contractor_Name from Ticket as t
join Client as c on t.Client_ID = c.Client_ID
join Contractor as con on t.Contractor_ID = con.Contractor_ID
join Priority_ as p on t.Priority_No = p.Priority_No
join Ticket_History as th on t.Problem_No = th.Problem_No
join Login_ as l on l.Log_ID = c.Log_ID
where l.User_Name_ = @uname and l.Password_ = @pword and t.Date_time_Raised between @from and @to;

exec Find_Ticket_History @uname = 'Jack_West', @pword = 'JW10002',  @from = '2019-01-01 00:00:00', @to = '2021-01-01 00:00:00';

--view all ticket History
--Drop procedure Find_Ticket_History_view
Create procedure Find_Ticket_History_view @uname varchar(60), @pword varchar(20)
as
select th.Update_Ref_No as Update_No, t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client_Name, 
t.Date_time_Raised, p.Priority_Desc,  th.Process, t.Subject_Title, t.Description_, t.Completed, t.Date_time_completed, 
(con.First_Name + ' ' + con.Last_Name) as Contractor_Name from Ticket as t
join Client as c on t.Client_ID = c.Client_ID
join Contractor as con on t.Contractor_ID = con.Contractor_ID
join Priority_ as p on t.Priority_No = p.Priority_No
join Ticket_History as th on t.Problem_No = th.Problem_No
join Login_ as l on l.Log_ID = c.Log_ID
where l.User_Name_ = @uname and l.Password_ = @pword

exec Find_Ticket_History_view @uname = 'Jack_West', @pword = 'JW10002'

--search login
--Drop procedure Find_Login
create procedure Find_Login @uname varchar(60), @pword varchar(20)
as
select l.Log_ID, l.User_Name_, l.Password_ from Login_ as l
where @uname = l.User_Name_ and @pword = l.Password_

exec Find_Login @uname = 'Mark_Baxton', @pword = 'MB10001'

--search login admin
Drop procedure Find_Login_Admin
create procedure Find_Login_Admin @uname varchar(60), @pword varchar(20)
as
select l.Log_ID, l.User_Name_, l.Password_, c.Contractor_ID from Contractor as c
join Login_ as l on c.Log_ID = l.Log_ID
where @uname = l.User_Name_ and @pword = l.Password_

exec Find_Login_Admin @uname = 'Tiffany_Norris', @pword = 'TN10005'
exec Find_Login_Admin @uname = 'Jack_West', @pword = 'JW10002'

--view all Tickets for loggedin Clients
--Drop procedure Get_Tickets_Client
create procedure Get_Tickets_Client @uname varchar(60), @pword varchar(20)
as
select t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client,  t.Date_time_Raised, p.Priority_Desc, t.Subject_Title, 
(con.First_Name + ' ' + con.Last_Name) as Contractor, t.Completed, t.Date_time_completed from Ticket as t
join Contractor as con on con.Contractor_ID = t.Contractor_ID
join Client as c on c.Client_ID = t.Client_ID
join Login_ as l on l.Log_ID = c.Log_ID
join Priority_ as p on p.Priority_No = t.Priority_No
where l.User_Name_ = @uname and l.Password_ = @pword

exec Get_Tickets_Client @uname = 'Jack_West', @pword = 'JW10002'

--view all Tickets Histroy for loggedin Clients
--Drop procedure Get_Tickets_Client_History
create procedure Get_Tickets_Client_History @uname varchar(60), @pword varchar(20)
as
select t.Problem_No, t.Process as State_, (c.First_Name + ' ' + c.Last_Name) as Client,  t.Date_time_Raised, p.Priority_Desc, 
t.Update_Ref_No as Update_No, t.Subject_Title, (con.First_Name + ' ' + con.Last_Name) as Contractor, t.Completed, t.Date_time_completed 
from Ticket_History as t
join Contractor as con on con.Contractor_ID = t.Contractor_ID
join Client as c on c.Client_ID = t.Client_ID
join Login_ as l on l.Log_ID = c.Log_ID
join Priority_ as p on p.Priority_No = t.Priority_No
where l.User_Name_ = @uname and l.Password_ = @pword

exec Get_Tickets_Client_History @uname = 'Jack_West', @pword = 'JW10002'

--get client
create procedure Get_Client @uname varchar(60), @pword varchar(20)
as
select c.Client_ID, (c.First_Name + ' ' + c.Last_Name) as Client_Name, 
d.Department_Name, c.Phone, c.Email from Client as c
join Department as d on d.Department_No = c.Department_No
join Login_ as l on l.Log_ID = c.Log_ID
where l.User_Name_ = @uname and l.Password_ = @pword

exec Get_Client @uname = 'Jack_West', @pword = 'JW10002'

--Get Discription
create procedure Get_Desc @problemNo int
as
select t.Subject_Title, t.Description_ from Ticket as t
where t.Problem_No = @problemNo

exec Get_Desc @problemNo = 10001

--gets all tickets assigned to the adimn
Drop procedure Get_Tickets_Admin
create procedure Get_Tickets_Admin @uname varchar(60), @pword varchar(20)
as
select t.Problem_No, (c.First_Name + ' ' + c.Last_Name) as Client_Name,  t.Subject_Title, (con.First_Name + ' ' + con.Last_Name) as Contractor_Name, p.Priority_Desc, t.Completed 
from Ticket as t
join Client as c on c.Client_ID = t.Client_ID
join Contractor as con on con.Contractor_ID = t.Contractor_ID
join Priority_ as p on p.Priority_No = t.Priority_No
join Login_ as l on l.Log_ID = con.Log_ID
where l.User_Name_ = @uname and l.Password_ = @pword or t.Contractor_ID = 10000

exec Get_Tickets_Admin @uname = 'Tobi_Knights', @pword = 'TK10007'


--Get History Discription
--Drop procedure Get_History_Desc
create procedure Get_History_Desc @UpdateNo int
as
select th.Subject_Title, th.Description_ from Ticket_History as th
where th.Update_Ref_No = @UpdateNo

exec Get_History_Desc @UpdateNo = 10000

--Get Admin via word search
--Drop procedure Get_Contractor
create procedure Get_Contractor @word varchar(MAX)
as
select c.Contractor_ID, k.Word, s.Spcialties_Name from Key_Words as k
join Spcialty_Key_Words as sw on sw.Word_ID = k.Word_ID
join Spcialties as s on s.Spcialties_ID = sw.Spcialties_ID
join Contractor_Spcialties as cs on cs.Spcialties_ID = s.Spcialties_ID
join Contractor as c on c.Contractor_ID = cs.Contractor_ID
where k.Word = @word and k.Ignore_Word = 0
order by c.Contractor_ID 

exec Get_Contractor @word = 'Server'


--Drop procedure Check_Key_Words
create procedure Check_Key_Words @word varchar (MAX)
as
select * from Key_Words as k
where upper(k.Word) = upper(@word)

exec Check_Key_Words @word = 'server'

--Drop procedure Check_New_Key_Words
create procedure Check_New_Key_Words @word varchar (MAX)
as
select * from New_Key_Words as k
where upper(k.Word) = upper(@word)

exec Check_New_Key_Words @word = 'server'

--Drop procedure shouldIgnore
create procedure shouldIgnore @word varchar (MAX)
as
select * from Key_Words as k
where upper(k.Word) = upper(@word) and k.Ignore_Word = 0

exec shouldIgnore @word = 'server'

--get word based on id or ignore
create procedure Key_Word_Search_Num @word int
as
select * from Key_Words
where Word_ID = @word or Ignore_Word = @word

exec Key_Word_Search_Num @word = 110
exec Key_Word_Search_Num @word = 0

--get word based on characters 
create procedure Key_Word_Search_Varchar @word varchar(30)
as
select * from Key_Words
where UPPER(Word) = UPPER(@word)

exec Key_Word_Search_Varchar @word = 'pc'





--gets all IDs and Names of Contractors
create view ComboBox_Contractor as c
as
select con.Contractor_ID, (con.First_Name + ' ' + con.Last_Name) as Contractor_Name from Contractor as con

select * from ComboBox_Contractor


--gets all IDs and Names of Clients
create view ComboBox_Client 
as
select c.Client_ID, (c.First_Name + ' ' + c.Last_Name) as Client_Name from Client as c

select * from ComboBox_Client

--get all Clients
create view ViewAllClients
as
select c.Client_ID, (c.First_Name + ' ' + c.Last_Name) as Client_Name, d.Department_Name, c.Email, c.Phone from Client as c
join Department as d on d.Department_No = c.Department_No

select * from ViewAllClients

--get all word to admin associations
create view Get_All_Associations
as
select skw.Spcialties_ID, s.Spcialties_Name, skw.Word_ID, k.Word from Spcialty_Key_Words as skw
join Key_Words as k on k.Word_ID = skw.Word_ID
join Spcialties as s on s.Spcialties_ID = skw.Spcialties_ID

select * from Get_All_Associations

--Triggers
--drop trigger Ticket_History_Insert
Create Trigger Ticket_History_Insert 
on Ticket
after Insert
as
Begin

declare @Problem_No int
declare @Client_ID int
declare @Date_Time_Raised Datetime
declare @Priority_No int
declare @Subject_Title varchar(60)
declare @Description_ varchar(Max)
declare @Completed tinyint
declare @Date_Time_Completed Datetime
declare @Contractor_ID int

declare @Update_Ref_No int
select @Update_Ref_No = (select top 1 Update_Ref_No from Ticket_History order by Update_Ref_No desc) + 1001

select @Problem_No = inserted.Problem_No from inserted
select @Client_ID = inserted.Client_ID from inserted
select @Date_Time_Raised = inserted.Date_time_Raised from inserted
select @Priority_No = inserted.Priority_No from inserted
select @Subject_Title = inserted.Subject_Title from inserted
select @Description_ = inserted.Description_ from inserted
select @Completed = inserted.Completed from inserted
select @Date_Time_Completed = inserted.Date_time_completed from inserted
select @Contractor_ID = inserted.Contractor_ID from inserted

insert into Ticket_History 
(Update_Ref_No, Problem_No, Client_ID, Date_time_Raised, Priority_No, Subject_Title, Description_, Completed,
Date_time_completed, Contractor_ID, Process, Date_Time)
values
(@Update_Ref_No, 
@Problem_No, @Client_ID, @Date_Time_Raised, @Priority_No, 
@Subject_Title, @Description_, @Completed, @Date_Time_Completed, 
@Contractor_ID, 'Added', GetDate())
end;

--drop trigger Ticket_History_Update
Create Trigger Ticket_History_Update 
on Ticket
after Insert
as
Begin

declare @Problem_No int
declare @Client_ID int
declare @Date_Time_Raised Datetime
declare @Priority_No int
declare @Subject_Title varchar(60)
declare @Description_ varchar(Max)
declare @Completed tinyint
declare @Date_Time_Completed Datetime
declare @Contractor_ID int

declare @Update_Ref_No int
select @Update_Ref_No = (select top 1 Update_Ref_No from Ticket_History order by Update_Ref_No desc) + 1001

select @Problem_No = inserted.Problem_No from inserted
select @Client_ID = inserted.Client_ID from inserted
select @Date_Time_Raised = inserted.Date_time_Raised from inserted
select @Priority_No = inserted.Priority_No from inserted
select @Subject_Title = inserted.Subject_Title from inserted
select @Description_ = inserted.Description_ from inserted
select @Completed = inserted.Completed from inserted
select @Date_Time_Completed = inserted.Date_time_completed from inserted
select @Contractor_ID = inserted.Contractor_ID from inserted

insert into Ticket_History 
(Update_Ref_No, Problem_No, Client_ID, Date_time_Raised, Priority_No, Subject_Title, Description_, Completed,
Date_time_completed, Contractor_ID, Process, Date_Time)
values
(@Update_Ref_No, 
@Problem_No, @Client_ID, @Date_Time_Raised, @Priority_No, 
@Subject_Title, @Description_, @Completed, @Date_Time_Completed, 
@Contractor_ID, 'Updated', GetDate())
end;

/*
disable trigger Ticket.Ticket_History_Insert on Ticket
go
--disable trigger Ticket.Ticket_History_Remove on Ticket
--go
disable trigger Ticket.Ticket_History_Update on Ticket
go



enable trigger Ticket.Ticket_History_Insert on Ticket
go
--enable trigger Ticket.Ticket_History_Remove on Ticket
--go
enable trigger Ticket.Ticket_History_Update on Ticket
go

--This code is only for testing Add, Update and Delete
disable trigger Ticket.Ticket_History_Insert on Ticket
go
--disable trigger Ticket.Ticket_History_Remove on Ticket
--go
*/

disable trigger Ticket.Ticket_History_Update on Ticket
go

disable trigger Ticket.Ticket_History_Update on Ticket
go

delete from Ticket_History
where Problem_No = 10004
go



enable trigger Ticket.Ticket_History_Insert on Ticket
go
--enable trigger Ticket.Ticket_History_Remove on Ticket
--go
enable trigger Ticket.Ticket_History_Update on Ticket
go

delete from Ticket
delete from Ticket_History
delete from Contractor_Spcialties
delete from Contractor
delete from Client

insert into Login_ values (109, 'test', 'test')
insert into Client values (10009, 'test', 'tset', 1, 'test', 'test', 109)
insert into Contractor values (10009, 'test', 'test', 1, 'test', 'test', 109, 10009)
insert into Ticket values (10006, 10002, '2021-11-09 11:50:00', 3, 'Printer is not working', 'The printer is out of ink and paper.', 0, null, 10009)

delete from Ticket_History
where Problem_No = 10004
go

delete from Client
where Client_ID = 10009
go

select * from Login_
select * from Client
select * from Contractor
select * from Contractor_Spcialties
select * from Spcialties
select * from Department
select * from Ticket
select * from Ticket_History
select * from Priority_
select * from New_Key_Words
select * from Key_Words
select * from Spcialty_Key_Words

select (c.First_Name + ' ' + c.Last_Name) name_, s.Spcialties_Name from Contractor_Spcialties as cs
join Spcialties as s on cs.Spcialties_ID = s.Spcialties_ID
join Contractor as c on cs.Contractor_ID = c.Contractor_ID