
USE workspace_crm
GO


CREATE FUNCTION sfGetStringAgg (@PersonID int, @TableName varchar(20), @FieldName varchar(20), @IDFieldName varchar(20))
RETURNS varchar(100)
AS
BEGIN
	DECLARE @result varchar(100)
	SET @result = (SELECT STRING_AGG(@FieldName,', ') FROM @TableName WHERE @IDFieldName = @PersonID)
	RETURN @result
END
GO

USE workspace_crm
GO
CREATE PROCEDURE spAddUser
	@UserName [nvarchar](20),
	@Password [varchar](20)
AS
BEGIN
	INSERT INTO LoginData VALUES (@UserName, @Password)
	INSERT INTO AccessRigths (UserName) VALUES (@UserName)
END
GO

/* 
Создадим процедуру, которая будет возвращать нам клиентов по фамилии или фрагменту фамилии.
*/

SELECT * FROM EmailAddresses
GO

USE workspace_crm
GO

ALTER PROCEDURE spClientsFilter
	@SearchSubstring [nvarchar](20)
AS
	BEGIN
		SELECT *
		FROM People
		WHERE Surname LIKE '%' + @SearchSubstring + '%'
	END
GO

EXEC spClientsFilter 'ас'

DROP PROCEDURE spClientsFilter

SELECT
	FirstName,
	Surname,
	DateOfBirth,
	STRING_AGG (Number, ', ') AS phoneNumbers,
	STRING_AGG (EMail, ', ') AS emails 
FROM People p
join PhoneNumbers n ON p.PersonID = n.PersonID
join EmailAddresses e ON n.PersonID = e.PersonID
GROUP BY p.PersonID, FirstName, Surname, DateOfBirth

SELECT
	STRING_AGG (Email,', ') AS emails
FROM EmailAddresses
GROUP BY PersonID

SELECT
	STRING_AGG (Number,', ') AS phoneNumber
FROM PhoneNumbers
GROUP BY PersonID

--Надо разбираться
SELECT
	STRING_AGG (Number,', ') AS phoneNumbers,
	STRING_AGG (Email,', ') AS emails
FROM PhoneNumbers INNER JOIN EmailAddresses ON PhoneNumbers.PersonID = EmailAddresses.PersonID
GROUP BY PhoneNumbers.PersonID

EXEC spClientsFilter 'А'

EXEC spAddUser 'developer','farcjst'
EXEC spAddUser 'Швец Анастасия','NASTYA1975'
EXEC spAddUser 'Шилкина Мария','moscow'
EXEC spAddUser 'Каниболоцкая Ирина','FKTRCFYLH'
GO

CREATE PROCEDURE spGetAccessRigths
	@UserName [nvarchar](20)
AS
BEGIN
	SELECT ViewClientDetails, EditClientDetails FROM AccessRigths WHERE UserName = @UserName
END


CREATE PROCEDURE spAddEmail
						@Email [nvarchar](50),
						@Surname [nvarchar](20)
AS
	BEGIN
		INSERT INTO EmailAddresses (Email, PersonID) VALUES
		(@Email, (SELECT PersonID FROM People WHERE Surname = @Surname))
	END
GO
