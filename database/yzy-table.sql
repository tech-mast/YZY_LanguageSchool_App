
/* ***** CREATE DATABASE ***** */

USE master;
 GO

 DROP DATABASE IF EXISTS yzyschool;
 GO




 CREATE DATABASE yzyschool
 ON PRIMARY
 (
	NAME = 'YZY_PRM',
	FILENAME = 'E:\Ying\JohnAbbott\IPD-24\Database\YZY_PRM.MDF',
	SIZE = 1MB,
	MAXSIZE = 64MB,
	FILEGROWTH = 1MB
 ),
FILEGROUP YZY_FG
  ( NAME = 'YZY_FG_DAT1',
    FILENAME =
       'E:\Ying\JohnAbbott\IPD-24\Database\YZY_FG_1.NDF',
    SIZE = 1MB,
    MAXSIZE=64MB,
    FILEGROWTH=1MB
	),
  ( NAME = 'YZYn_FG_DAT2',
    FILENAME =
       'E:\Ying\JohnAbbott\IPD-24\Database\YZY_FG_2.NDF',
    SIZE = 1MB,
    MAXSIZE=64MB,
    FILEGROWTH=1MB
	)
 LOG ON
 (
	NAME = 'YZY_LOG',
	FILENAME = 'E:\Ying\JohnAbbott\IPD-24\Database\YZY_LOG.LDF',
	SIZE = 1MB,
	MAXSIZE = 16MB,
	FILEGROWTH = 1MB
 )
 COLLATE SQL_Latin1_General_CP1_CI_AS; -- latin, case insensitive, accent sensitive
 GO





/* ***** CREATE TABELS  ***** */


USE yzyschool;
GO

/* ***** 1. create the Users table  ***** */

DROP TABLE IF EXISTS Users;
GO

CREATE TABLE Users
(
UserID INT IDENTITY(1,1) NOT NULL,
UserRole INT NOT NULL , -- In order to generate Enum in EntityFramework, need to set data type in INT. 1:'Admin', 2:'Teacher', 3:'Student'
FName NVARCHAR(30) NOT NULL,
MName NVARCHAR(30) NULL,
LName NVARCHAR(30) NOT NULL,
UserSIN NCHAR(9) MASKED WITH (FUNCTION = 'DEFAULT()') NOT NULL,
Gender INT NOT NULL, -- In order to generate Enum in EntityFramework, need to set data type in )INT. 1:'Female', 2:'Male', 3:'Other'
StreetNo  NVARCHAR(50) NOT NULL,
StreetName NVARCHAR(50) NOT NULL,
City NVARCHAR(30) NOT NULL,
Province NCHAR(2) NOT NULL, 
PostalCode NCHAR(6) NOT NULL,
Phone NCHAR(10) NULL,
Cell NCHAR(10) NOT NULL,
Email  NVARCHAR(20) not null,
Photo VARBINARY(MAX) null,
Password NVARCHAR(20) MASKED WITH (FUNCTION = 'DEFAULT()') NOT NULL,

CONSTRAINT PK_USERS PRIMARY KEY (UserID),
CONSTRAINT UQ_USERS_SIN UNIQUE (UserSIN),
CONSTRAINT CK_USERS_PROV CHECK 
		(Province IN ('QC', 'ON', 'BC', 'NL', 'PE', 'NS', 'NB', 'MB', 'SK', 'AB', 'YT', 'NT', 'NU')),
CONSTRAINT CK_USERS_PHONE CHECK (Phone LIKE REPLICATE('[0-9]', 10)),
CONSTRAINT CK_USERS_CELL CHECK (Cell LIKE REPLICATE('[0-9]', 10)),
CONSTRAINT CK_USERS_POST CHECK (PostalCode LIKE '[A-Z][0-9][A-Z][0-9][A-Z][0-9]')
);
GO


/* ***** 2. create the Payments table  ***** */

DROP TABLE IF EXISTS Payments;
GO

CREATE TABLE Payments
(
PaymentID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
PayAccount NVARCHAR(30) NOT NULL,
Amount Money NOT NULL,
PayDate DATETIME not null,

CONSTRAINT PK_PAYMENTS PRIMARY KEY (PaymentID)
);
GO



/* ***** 3. create the Categories table  ***** */
DROP TABLE IF EXISTS Categories;
GO

CREATE TABLE Categories
(
CategoryID INT IDENTITY(1,1) NOT NULL,
CateDesc NVARCHAR(50) NOT NULL,
Difficulty SMALLINT not null -- 0:Beginner, 1:Intermediate, 2:...

CONSTRAINT PK_CATEGORIES PRIMARY KEY (CategoryID)
)
;


/* ***** 4. create the Courses table  ***** */
DROP TABLE IF EXISTS Courses;
GO

CREATE TABLE Courses
(
CourseID INT IDENTITY(1,1) NOT NULL,
CategoryID INT not null, -- Fk Categories
UserID INT not null, -- fk users, only for teacher
CourseDesc NVARCHAR(100) not null,
StartDate Date NOT NULL,
EndDate Date NOT NULL,
Tuition Money not null,

CONSTRAINT PK_COURSES PRIMARY KEY (CourseID)
)
;


/* ***** 5. create the Registers table  ***** */
DROP TABLE IF EXISTS Registers;
GO

CREATE TABLE Registers
(
RegisterID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL, -- Fk Users , only for Students
CourseID INT NOT NULL, -- Fk Courses
RegisterStatus INT not null , -- In order to generate Enum in EntityFramework, need to set data type in INT. 1:'Pending', 2:'Done', 3:'Cancelled'
Grade VARCHAR(2) NULL,


CONSTRAINT PK_REGISTERS PRIMARY KEY (RegisterID)
)
;


/* ***** 6. create the Evaluations table  ***** */
DROP TABLE IF EXISTS Evaluations;
GO

CREATE TABLE Evaluations
(
EvaluationID  INT IDENTITY(1,1) NOT NULL,
RegisterID	INT NOT NULL, -- fk Registers
EvDate DATETIME not null,
Attachment VARBINARY null,
Comment NVARCHAR(200) not null,
CONSTRAINT PK_EVALUATIONS PRIMARY KEY (EvaluationID)


);

GO


/* ***** CREATE DATA INTEGRITY ***** */

/* ***** 2. alter the Payments table  ***** */

ALTER TABLE Payments
	ADD 
	CONSTRAINT FK_PAYMENTS_USERS FOREIGN KEY (UserID) 
	REFERENCES Users(UserID)

GO
;


/* ***** 4. alter the Courses table  ***** */
ALTER TABLE Courses
	ADD 
	CONSTRAINT FK_COURSES_USERS FOREIGN KEY (UserID) 
	REFERENCES Users(UserID), -- Only for teacher role in Users table

    CONSTRAINT FK_COURSES_CATEGORIES FOREIGN KEY (CategoryID) 
	REFERENCES CATEGORIES(CategoryID)

GO
;


/* ***** 5. alter the Registers table  ***** */

ALTER TABLE REGISTERS
	ADD 
	CONSTRAINT FK_REGISTERS_USERS FOREIGN KEY (UserID) 
	REFERENCES Users(UserID), 

    CONSTRAINT FK_REGISTERS_COURSES FOREIGN KEY (CourseID) 
	REFERENCES COURSES(CourseID)

GO
;


/* ***** 6. alter the Evaluations table  ***** */

ALTER TABLE EVALUATIONS
	ADD 
	CONSTRAINT FK_EVALUATIONS_REGISTERS FOREIGN KEY (RegisterID) 
	REFERENCES REGISTERS(RegisterID)

GO
;



/* ***** IMPORT DUMMY DATA FROM CSV FILES ***** */

 BULK INSERT Users FROM 'C:\IPD\yzy\technologies\Dummy Data\Users.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO


 BULK INSERT Categories FROM 'C:\IPD\yzy\technologies\Dummy Data\Categories.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO

 BULK INSERT Courses FROM 'C:\IPD\yzy\technologies\Dummy Data\Courses.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO

 BULK INSERT Payments FROM 'C:\IPD\yzy\technologies\Dummy Data\Payments.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO

 BULK INSERT Registers FROM 'C:\IPD\yzy\technologies\Dummy Data\Registers.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO

 BULK INSERT Evaluations FROM 'C:\IPD\yzy\technologies\Dummy Data\Evaluations.csv'
   WITH (
      FIELDTERMINATOR = ',',
      ROWTERMINATOR = '\n',
	  		KEEPNULLS,
		FIRSTROW = 2
);
GO