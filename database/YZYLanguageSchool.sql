/****** Object:  Table [dbo].[Categories]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CateDesc] [nvarchar](50) NOT NULL,
	[Difficulty] [smallint] NOT NULL,
 CONSTRAINT [PK_CATEGORIES] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseDesc] [nvarchar](100) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Tuition] [money] NOT NULL,
 CONSTRAINT [PK_COURSES] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserRole] [int] NOT NULL,
	[FName] [nvarchar](30) NOT NULL,
	[MName] [nvarchar](30) NULL,
	[LName] [nvarchar](30) NOT NULL,
	[UserSIN] [nchar](9) MASKED WITH (FUNCTION = 'default()') NOT NULL,
	[Gender] [int] NOT NULL,
	[StreetNo] [nvarchar](50) NOT NULL,
	[StreetName] [nvarchar](50) NOT NULL,
	[City] [nvarchar](30) NOT NULL,
	[Province] [nchar](2) NOT NULL,
	[PostalCode] [nchar](6) NOT NULL,
	[Phone] [nchar](10) NULL,
	[Cell] [nchar](10) NOT NULL,
	[Email] [nvarchar](20) NOT NULL,
	[Photo] [varbinary](max) NULL,
	[Password] [nvarchar](20) MASKED WITH (FUNCTION = 'default()') NOT NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_USERS_SIN] UNIQUE NONCLUSTERED 
(
	[UserSIN] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vOpenCourses]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vOpenCourses]
AS
SELECT C.CourseID,C.CourseDesc,cg.CateDesc,c.StartDate,c.EndDate,c.Tuition,CONCAT_WS(' ',u.FName,ISNULL(u.MName,''),u.LName) as Teacher
FROM Courses AS C
INNER JOIN Users AS U
ON C.UserID = U.UserID
INNER JOIN Categories AS CG
ON c.CategoryID = CG.CategoryID
WHERE c.StartDate > GETDATE();
;
GO
/****** Object:  Table [dbo].[Registers]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Registers](
	[RegisterID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[RegisterStatus] [int] NOT NULL,
	[Grade] [varchar](2) NULL,
 CONSTRAINT [PK_REGISTERS] PRIMARY KEY CLUSTERED 
(
	[RegisterID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vStudentRegister]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vStudentRegister]
AS
SELECT U.UserID,cg.CateDesc,R.RegisterID,cg.Difficulty,c.CourseID,c.CourseDesc,c.StartDate,c.EndDate,c.Tuition, r.RegisterStatus ,CONCAT_WS(' ',u2.FName,ISNULL(u2.MName,''),u2.LName) as Teacher
FROM Users AS U
INNER JOIN Registers AS R
ON U.UserID = R.UserID
INNER JOIN Courses AS C
ON R.CourseID = C.CourseID
INNER JOIN Categories AS CG
ON C.CategoryID= CG.CategoryID
INNER JOIN Users AS U2
ON C.UserID = U2.UserID
;
GO
/****** Object:  Table [dbo].[Evaluations]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluations](
	[EvaluationID] [int] IDENTITY(1,1) NOT NULL,
	[RegisterID] [int] NOT NULL,
	[EvDate] [datetime] NOT NULL,
	[Attachment] [varbinary](1) NULL,
	[Comment] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_EVALUATIONS] PRIMARY KEY CLUSTERED 
(
	[EvaluationID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 2021-04-20 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PayAccount] [nvarchar](30) NOT NULL,
	[Amount] [money] NOT NULL,
	[PayDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PAYMENTS] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Courses]  WITH NOCHECK ADD  CONSTRAINT [FK_COURSES_CATEGORIES] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_COURSES_CATEGORIES]
GO
ALTER TABLE [dbo].[Courses]  WITH NOCHECK ADD  CONSTRAINT [FK_COURSES_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_COURSES_USERS]
GO
ALTER TABLE [dbo].[Evaluations]  WITH NOCHECK ADD  CONSTRAINT [FK_EVALUATIONS_REGISTERS] FOREIGN KEY([RegisterID])
REFERENCES [dbo].[Registers] ([RegisterID])
GO
ALTER TABLE [dbo].[Evaluations] CHECK CONSTRAINT [FK_EVALUATIONS_REGISTERS]
GO
ALTER TABLE [dbo].[Payments]  WITH NOCHECK ADD  CONSTRAINT [FK_PAYMENTS_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_PAYMENTS_USERS]
GO
ALTER TABLE [dbo].[Registers]  WITH NOCHECK ADD  CONSTRAINT [FK_REGISTERS_COURSES] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Courses] ([CourseID])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_REGISTERS_COURSES]
GO
ALTER TABLE [dbo].[Registers]  WITH NOCHECK ADD  CONSTRAINT [FK_REGISTERS_USERS] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Registers] CHECK CONSTRAINT [FK_REGISTERS_USERS]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [CK_USERS_CELL] CHECK  (([Cell] like replicate('[0-9]',(10))))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_USERS_CELL]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [CK_USERS_PHONE] CHECK  (([Phone] like replicate('[0-9]',(10))))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_USERS_PHONE]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [CK_USERS_POST] CHECK  (([PostalCode] like '[A-Z][0-9][A-Z][0-9][A-Z][0-9]'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_USERS_POST]
GO
ALTER TABLE [dbo].[Users]  WITH NOCHECK ADD  CONSTRAINT [CK_USERS_PROV] CHECK  (([Province]='NU' OR [Province]='NT' OR [Province]='YT' OR [Province]='AB' OR [Province]='SK' OR [Province]='MB' OR [Province]='NB' OR [Province]='NS' OR [Province]='PE' OR [Province]='NL' OR [Province]='BC' OR [Province]='ON' OR [Province]='QC'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_USERS_PROV]
GO
