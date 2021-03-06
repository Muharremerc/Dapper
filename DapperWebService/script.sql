USE [School]
GO
/****** Object:  Table [dbo].[Lesson]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Lesson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTeacherLesson]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTeacherLesson](
	[TeacherLessonId] [int] NOT NULL,
	[StudentId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherLesson]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherLesson](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NULL,
	[TeacherId] [int] NULL,
 CONSTRAINT [PK_TeacherLesson] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StudentTeacherLesson]  WITH CHECK ADD  CONSTRAINT [FK_StudentTeacherLesson_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[StudentTeacherLesson] CHECK CONSTRAINT [FK_StudentTeacherLesson_Student]
GO
ALTER TABLE [dbo].[StudentTeacherLesson]  WITH CHECK ADD  CONSTRAINT [FK_StudentTeacherLesson_TeacherLesson] FOREIGN KEY([TeacherLessonId])
REFERENCES [dbo].[TeacherLesson] ([Id])
GO
ALTER TABLE [dbo].[StudentTeacherLesson] CHECK CONSTRAINT [FK_StudentTeacherLesson_TeacherLesson]
GO
ALTER TABLE [dbo].[TeacherLesson]  WITH CHECK ADD  CONSTRAINT [FK_TeacherLesson_Lesson] FOREIGN KEY([LessonId])
REFERENCES [dbo].[Lesson] ([Id])
GO
ALTER TABLE [dbo].[TeacherLesson] CHECK CONSTRAINT [FK_TeacherLesson_Lesson]
GO
ALTER TABLE [dbo].[TeacherLesson]  WITH CHECK ADD  CONSTRAINT [FK_TeacherLesson_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[TeacherLesson] CHECK CONSTRAINT [FK_TeacherLesson_Teacher]
GO
/****** Object:  StoredProcedure [dbo].[GetStudentLessonsListById]    Script Date: 15.03.2022 11:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentLessonsListById] @Id int
AS
SELECT S.Id AS 'StudentId',S.Name+' '+S.Surname 'StudentName' ,T.Name AS 'TeacherName',L.Name AS 'LessonName' FROM Teacher AS T	
                                     JOIN TeacherLesson AS TL ON TL.TeacherId = T.Id
                                          JOIN Lesson AS L ON L.Id = TL.LessonId
                                               JOIN StudentTeacherLesson STL ON STL.TeacherLessonId = TL.Id
                                                    JOIN Student AS S ON S.Id = STL.StudentId Where S.Id = @Id
GO
