CREATE VIEW Teachers
AS SELECT * FROM People WHERE RoleID = 3
GO

CREATE VIEW Students
AS SELECT * FROM People WHERE RoleID = 1
GO

SELECT * FROM Students
SELECT * FROM Teachers

