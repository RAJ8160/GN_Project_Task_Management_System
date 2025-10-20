Create Database GN_Project_TMS

Create Table Users(
	UserID int   IDENTITY(1,1) PRIMARY KEY,
	UserName NVARCHAR(50) NOT NULL UNIQUE,
	Email  NVARCHAR(100) NOT NULL UNIQUE,
	PasswordHash NVARCHAR(150) NOT NULL,
	CreatedAt DATETIME DEFAULT GETDATE(),
	ActiveUser Bit DEFAULT 1
);

INSERT INTO Users (UserName, Email, PasswordHash) VALUES
('raj_r', 'raj@example.com', 'hash123'),
('neha_p', 'neha@example.com', 'hash456'),
('arjun_m', 'arjun@example.com', 'hash789'),
('meera_k', 'meera@example.com', 'hash101'),
('vishal_t', 'vishal@example.com', 'hash102'),
('sara_s', 'sara@example.com', 'hash103'),
('amit_p', 'amit@example.com', 'hash104'),
('ravi_d', 'ravi@example.com', 'hash105'),
('kiran_b', 'kiran@example.com', 'hash106'),
('nisha_l', 'nisha@example.com', 'hash107');

--DROP TABLE Users
--Select * from Users

CREATE TABLE Roles(
	RoleID int IDENTITY(1,1) PRIMARY KEY,
	RoleName NVARCHAR(50) NOT NULL UNIQUE,
	ActiveRole BIT DEFAULT 1
);
INSERT INTO Roles (RoleName) VALUES
('Admin'),
('Project Manager'),
('Developer'),
('Tester'),
('Designer'),
('Scrum Master'),
('Client'),
('Support'),
('Analyst'),
('DevOps');

--DROP TABLE Roles
Select * from Roles

CREATE TABLE UserRoles(
	UserRoleID INT IDENTITY(1,1) PRIMARY KEY,
	UserID INT FOREIGN KEY REFERENCES Users(UserID),
	RoleID INT FOREIGN KEY REFERENCES Roles(RoleID),
	ActiveUserRole BIT DEFAULT 1
);
INSERT INTO UserRoles (UserID, RoleID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

--DROP TABLE UserRoles
--Select * from UserRoles

CREATE TABLE Projects(
	ProjectID INT IDENTITY(1,1) PRIMARY KEY,
	ProjectName NVARCHAR(100) NOT NULL,
	Description NVARCHAR(255) NULL,
	CreatedBy INT FOREIGN KEY REFERENCES Users(UserID),
	CreatedAt DATETIME DEFAULT GETDATE(),
	ActiveProject BIT DEFAULT 1
);

INSERT INTO Projects (ProjectName, Description, CreatedBy) VALUES
('Task Management System', 'Issue tracking and collaboration tool', 1),
('E-Commerce App', 'Online store for electronics', 2),
('School ERP', 'Management software for schools', 3),
('HRMS System', 'Human resource management system', 4),
('Inventory Tracker', 'Tracks stock and suppliers', 5),
('Fitness Tracker', 'Mobile app for workouts', 6),
('Online Banking', 'Digital banking portal', 7),
('Travel Booking', 'Website for booking trips', 8),
('Restaurant POS', 'Point of sale management system', 9),
('Chat Application', 'Real-time messaging app', 10);

--DROP TABLE Projects
Select * from Projects

CREATE TABLE Sprints(
	SprintID INT IDENTITY(1,1) PRIMARY Key,
	ProjectID INT FOREIGN kEY REFERENCES Projects(ProjectID),
	SprintName NVARCHAR(100) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate Date NOT NULL,
	Status NVARCHAR(20) CHECK (Status IN ('Planned','Active','Closed')),
	ActiveSprint BIT DEFAULT 1
);
INSERT INTO Sprints (ProjectID, SprintName, StartDate, EndDate, Status) VALUES
(1, 'Sprint 1 - Setup', '2025-10-01', '2025-10-10', 'Closed'),
(1, 'Sprint 2 - UI', '2025-10-11', '2025-10-20', 'Active'),
(2, 'Sprint 1 - Backend', '2025-10-05', '2025-10-15', 'Closed'),
(3, 'Sprint 1 - Database', '2025-10-01', '2025-10-10', 'Closed'),
(4, 'Sprint 1 - Auth Module', '2025-10-03', '2025-10-12', 'Active'),
(5, 'Sprint 1 - Design', '2025-10-01', '2025-10-08', 'Planned'),
(6, 'Sprint 1 - Features', '2025-10-10', '2025-10-20', 'Active'),
(7, 'Sprint 1 - Setup', '2025-10-02', '2025-10-09', 'Closed'),
(8, 'Sprint 1 - API', '2025-10-04', '2025-10-14', 'Active'),
(9, 'Sprint 1 - POS Setup', '2025-10-05', '2025-10-15', 'Planned');

--DROP TABLE Sprints
--Select * From Sprints

CREATE TABLE IssueTypes(
	TypeID INT IDENTITY(1,1) PRIMARY KEY,
	TypeName NVARCHAR(50) NOT NULL,
	ActiveType BIT DEFAULT 1
);
INSERT INTO IssueTypes (TypeName) VALUES
('Bug'),
('Feature'),
('Task'),
('Improvement'),
('UI Enhancement'),
('Database'),
('Testing'),
('Deployment'),
('Documentation'),
('Security');
--Select * from IssueTypes
--DROP TABLE IssueTypes

CREATE TABLE Issues(
	IssueID INT IDENTITY(1,1) PRIMARY KEY,
	ProjectID INT FOREIGN KEY REFERENCES Projects(ProjectID),
	SprintID  INT FOREIGN KEY REFERENCES Sprints(SPrintID),
	TypeID  INT FOREIGN KEY REFERENCES IssueTypes(TypeID),
	AssignedTo INT FOREIGN KEY REFERENCES Users(UserID),
	Title NVARCHAR(100) NOT NULL,
	Description NVARCHAR(255) NULL,
	Priority	NVARCHAR(10)  CHECK (Priority IN ('Low','Medium','High')),
	Status	NVARCHAR(20)	CHECK (Status IN ('To Do','In Progress','Done')),
	CreatedAt	DATETIME	DEFAULT GETDATE(),
	ActiveIssue BIT DEFAULT 1
);

INSERT INTO Issues (ProjectID, SprintID, TypeID, AssignedTo, Title, Description, Priority, Status) VALUES
(1, 1, 1, 3, 'Login error', 'Fix invalid password issue', 'High', 'Done'),
(1, 2, 2, 4, 'Add dashboard', 'Create summary dashboard', 'Medium', 'In Progress'),
(2, 3, 3, 5, 'Setup backend API', 'Develop REST endpoints', 'High', 'Done'),
(3, 4, 4, 6, 'Optimize DB schema', 'Improve performance', 'Medium', 'To Do'),
(4, 5, 5, 7, 'Design login page', 'Improve look and feel', 'Low', 'In Progress'),
(5, 6, 6, 8, 'Fix stock sync bug', 'Sync quantities properly', 'High', 'To Do'),
(6, 7, 7, 9, 'Add workout logging', 'Enable daily logs', 'Medium', 'In Progress'),
(7, 8, 8, 10, 'Deploy to staging', 'Test environment setup', 'High', 'To Do'),
(8, 9, 9, 1, 'API documentation', 'Add API endpoints doc', 'Low', 'Done'),
(9, 10, 10, 2, 'Add SSL security', 'Implement HTTPS', 'High', 'In Progress');

--Select * from Issues
--DROP TABLE Issues

CREATE TABLE  Comments(
	CommentID INT IDENTITY(1,1) PRIMARY KEY,
	IssueID   INT FOREIGN KEY REFERENCES Issues(IssueID),
	UserID    INT FOREIGN KEY REFERENCES Users(UserID),
	CommentText	NVARCHAR(255)	NOT NULL,
	CreatedAt	DATETIME	DEFAULT GETDATE(),
	ActiveComment BIT DEFAULT 1
);

INSERT INTO Comments (IssueID, UserID, CommentText) VALUES
(1, 3, 'Bug fixed successfully.'),
(2, 4, 'Started working on UI.'),
(3, 5, 'API tested and verified.'),
(4, 6, 'Need clarification on DB schema.'),
(5, 7, 'Mockups ready for review.'),
(6, 8, 'Stock sync under testing.'),
(7, 9, 'Daily log feature half done.'),
(8, 10, 'Deployed to test environment.'),
(9, 1, 'Documentation updated.'),
(10, 2, 'SSL setup in progress.');

Select * From Comments
--DROP TABLE Comments

CREATE TABLE ActivityLog(
	LogID	INT  IDENTITY(1,1) PRIMARY KEY,
	IssueID	INT  FOREIGN KEY REFERENCES  Issues(IssueID),
	UserID	INT	 FOREIGN KEY REFERENCES  USers(UserID),
	ActionType	NVARCHAR(50)	NOT NULL,
	ActionTime	DATETIME	DEFAULT GETDATE(),
	ActiveActivityLog BIT DEFAULT 1
);
INSERT INTO ActivityLog (IssueID, UserID, ActionType) VALUES
(1, 3, 'Marked as Done'),
(2, 4, 'Status changed to In Progress'),
(3, 5, 'Closed issue'),
(4, 6, 'Added comment'),
(5, 7, 'Updated description'),
(6, 8, 'Assigned to user'),
(7, 9, 'Changed priority'),
(8, 10, 'Reopened issue'),
(9, 1, 'Added documentation'),
(10, 2, 'Changed status');

Select * from ActivityLog

--DROP TABLE ActivityLog
