USE master
ALTER DATABASE workspace_crm SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE workspace_crm
GO

CREATE DATABASE workspace_crm
GO

USE workspace_crm

CREATE TABLE PermissionsLists
	(PermissionsListID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 CanViewClientDetails [bit] NOT NULL DEFAULT (1),
	 CanEditClientDetails [bit] NOT NULL DEFAULT (0))

CREATE TABLE Roles
	(RoleID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 RoleName [nvarchar](30) NULL,
	 IsInternal [bit] NOT NULL, -- Cистемная роль (нельзя удалить)
	 PermissionsListID [int] NOT NULL FOREIGN KEY REFERENCES PermissionsLists (PermissionsListID)) 

CREATE TABLE People
	(PersonID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 Gender [nvarchar](10) NOT NULL,
	 FirstName [nvarchar](20) NOT NULL,
	 Patronymic [nvarchar](20) NULL,
	 Surname [nvarchar](20) NOT NULL,
	 DateOfBirth [date] NOT NULL,
	 RoleID [int] NOT NULL FOREIGN KEY REFERENCES Roles (RoleID),
	 PermissionsListID [int] NULL FOREIGN KEY REFERENCES PermissionsLists (PermissionsListID),
	 CONSTRAINT Clients_Gender CHECK ([Gender] IN ('мужской','женский')))

CREATE TABLE CaretakerRelations
	(
	 RelationID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 StudentID [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID),
	 CaretakerID [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID))

CREATE TABLE Rooms
([RoomID] [int] IDENTITY PRIMARY KEY CLUSTERED,
 [RoomName] [nvarchar](30) NOT NULL,
 [Capacity] [int] NOT NULL)

CREATE TABLE Units
([UnitID] [int] IDENTITY PRIMARY KEY CLUSTERED,
 [UnitType] [int] NOT NULL,
 [Capacity] [int] NOT NULL)

CREATE TABLE Binds
([BindID] [int] IDENTITY PRIMARY KEY CLUSTERED,
 [StudentID] [int] NOT NULL FOREIGN KEY REFERENCES [People] (PersonID),
 [UnitID] [int] NOT NULL FOREIGN KEY REFERENCES [Units] (UnitID),
 [StartDate] [date] NULL, -- Пока не используется (добавить NOT NULL)
 [EndDate] [date] NULL)   -- Пока не используется (добавить NOT NULL)

CREATE TABLE Lessons
([LessonID] [int] IDENTITY PRIMARY KEY CLUSTERED,
 [UnitID] [int] NOT NULL FOREIGN KEY REFERENCES [Units] (UnitID),
 [LessonDate] [date] NOT NULL,
 [TeacherID] [int] NOT NULL	FOREIGN KEY REFERENCES [People] (PersonID),
 [RoomID] [int] NOT NULL FOREIGN KEY REFERENCES [Rooms] (RoomID),
 [StartTime] [time] NOT NULL,
 [Duration] [time] NOT NULL)

CREATE TABLE VisitStatusList
	(VisitStatusID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 VisitStatusName [nvarchar](20))	 

CREATE TABLE Visits
([VisitID] [int] IDENTITY PRIMARY KEY CLUSTERED,
 [LessonID] [int] NOT NULL FOREIGN KEY REFERENCES [Lessons] (LessonID),
 [StudentID] [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID),
 [VisitStatusID] [int] NOT NULL FOREIGN KEY REFERENCES VisitStatusList (VisitStatusID))

CREATE TABLE PhoneNumbers
	(PhoneNumberID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 Number [varchar](16) NOT NULL,
	 PersonID [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID))

CREATE TABLE EmailAddresses
	(EmailID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 Email [varchar](30) NOT NULL,
	 PersonID [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID))

CREATE TABLE LoginDataList
	(LoginDataID [int] IDENTITY PRIMARY KEY CLUSTERED,	 
	 UserName [nvarchar](20) NOT NULL,
	 UserPassword [varchar](20) NOT NULL,
	 PersonID [int] NULL FOREIGN KEY REFERENCES People (PersonID))

CREATE TABLE CompanyAccounts
	(CompanyAccountID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 AccountName [nvarchar](20) NOT NULL,
	 Balance [decimal] NOT NULL)

CREATE TABLE Categories
	(CategoryID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 CategoryName [varchar](20) NOT NULL)

CREATE TABLE Counterparts
	(CounterpartID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 CounterpartName [nvarchar](30) NOT NULL)

CREATE TABLE AllAccounts
	(AccountID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 PersonID [int] NULL FOREIGN KEY REFERENCES People (PersonID),
	 CounterpartID [int] NULL FOREIGN KEY REFERENCES Counterparts (CounterpartID),
	 CompanyAccountID [int] NULL FOREIGN KEY REFERENCES CompanyAccounts (CompanyAccountID))

CREATE TABLE Transactions
	(TransactionID [int] IDENTITY PRIMARY KEY CLUSTERED,
	 TransactionDateTime [date] NOT NULL,
	 FromAccountID [int] NULL FOREIGN KEY REFERENCES AllAccounts (AccountID),
	 ToAccountID [int] NOT NULL FOREIGN KEY REFERENCES AllAccounts (AccountID),
	 TransactionType [varchar](10) NOT NULL,
	 TransactionSum [decimal] NOT NULL DEFAULT 0,
	 Discount [decimal] NOT NULL DEFAULT 0,
	 CategoryID [int] NOT NULL FOREIGN KEY REFERENCES Categories (CategoryID),
	 OperatorID [int] NOT NULL FOREIGN KEY REFERENCES People (PersonID), -- ссылаться на вьюшку, видимо, нельзя
	 RegisteredAt [date] NOT NULL DEFAULT (GETDATE()),
	 Comment [nvarchar](100) NULL,
	 CONSTRAINT TransactionType_Check CHECK ([TransactionType] IN ('Доход','Расход','Перевод')))