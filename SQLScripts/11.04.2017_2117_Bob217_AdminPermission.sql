UPDATE dbo.PermissionRoleKeys 
SET PermissionValue = 'true'
WHERE PermissionValue = 'false'
	AND PermissionRoleId IN
    (
 		SELECT PermissionRoleId
      	FROM dbo.EmployeePermissionRoles
      	WHERE EmployeeId IN
      (
      	SELECT Id
      	FROM dbo.Employees 
      	WHERE LastName = 'Шоранова'
      )
    )
	
UPDATE dbo.PermissionRoleKeys 
SET PermissionValue = 'all'
WHERE PermissionValue = 'employee'
	AND PermissionRoleId IN
    (
 		SELECT PermissionRoleId
      	FROM dbo.EmployeePermissionRoles
      	WHERE EmployeeId IN
      (
      	SELECT Id
      	FROM dbo.Employees 
      	WHERE LastName = 'Шоранова'
      )
    )
	