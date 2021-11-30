CREATE TABLE [dbo].[task_management]
(
	[project_ID ] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [project_Title] NVARCHAR(50) NULL, 
    [project_Status] NVARCHAR(50) NULL, 
    [project_Progress ] DECIMAL NULL, 
    [task_ID] NVARCHAR(50) NULL, 
    [task_Title] NVARCHAR(50) NULL, 
    [project_StartDate] DATETIME NULL, 
    [project_EndDate] DATETIME NULL, 
    [project_Desc ] NVARCHAR(50) NULL, 
    [task_Status] NVARCHAR(50) NULL, 
    [task_MemberID ] NVARCHAR(50) NULL, 
    [task_ManagerID] NVARCHAR(50) NULL
)
