# PilotTasks
USE [master]
GO
/****** Object:  Database [PilotTasksDB]    Script Date: 7/20/2024 9:38:08 AM ******/
CREATE DATABASE [PilotTasksDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PilotTasksDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PilotTasksDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PilotTasksDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PilotTasksDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PilotTasksDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PilotTasksDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PilotTasksDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PilotTasksDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PilotTasksDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PilotTasksDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PilotTasksDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PilotTasksDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PilotTasksDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PilotTasksDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PilotTasksDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PilotTasksDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PilotTasksDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PilotTasksDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PilotTasksDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PilotTasksDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PilotTasksDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PilotTasksDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PilotTasksDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PilotTasksDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PilotTasksDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PilotTasksDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PilotTasksDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PilotTasksDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PilotTasksDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PilotTasksDB] SET  MULTI_USER 
GO
ALTER DATABASE [PilotTasksDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PilotTasksDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PilotTasksDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PilotTasksDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PilotTasksDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PilotTasksDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PilotTasksDB] SET QUERY_STORE = OFF
GO
USE [PilotTasksDB]
GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
	[ProfileId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[EmailId] [nvarchar](50) NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[ProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NULL,
	[TaskName] [nvarchar](50) NULL,
	[TaskDescription] [nvarchar](50) NULL,
	[StartTime] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Tasks] FOREIGN KEY([Id])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Tasks]
GO
/****** Object:  StoredProcedure [dbo].[InsertOrUpdateProfile]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertOrUpdateProfile]
    @ProfileId INT = NULL, -- Use NULL for insert, provide Id for update
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DateOfBirth DATETIME,
    @PhoneNumber NVARCHAR(50),
    @EmailId NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF @ProfileId IS NULL OR @ProfileId = 0
    BEGIN
        -- Insert new record
        INSERT INTO Profiles (FirstName, LastName, DateOfBirth, PhoneNumber, EmailId)
        VALUES (@FirstName, @LastName, @DateOfBirth, @PhoneNumber, @EmailId);
    END
    ELSE
    BEGIN
        -- Update existing record
        UPDATE Profiles
        SET FirstName = @FirstName,
            LastName = @LastName,
            DateOfBirth = @DateOfBirth,
            PhoneNumber = @PhoneNumber,
            EmailId = @EmailId
        WHERE @ProfileId = @ProfileId;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteProfile]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_DeleteProfile]
    @ProfileId INT = NULL
  
AS
BEGIN
    SET NOCOUNT ON;

   Delete from Tasks where ProfileId = @ProfileId;
   Delete from Profiles where ProfileId = @ProfileId
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteTasks]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[usp_DeleteTasks]
    @Id INT = NULL
  
AS
BEGIN
    SET NOCOUNT ON;

   Delete from Tasks where Id = @Id
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetFilteredProfile]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetFilteredProfile]
    @ProfileId INT = NULL,
    @FirstName NVARCHAR(50) = NULL,
    @LastName NVARCHAR(50) = NULL,
    @DateOfBirth DATETIME = NULL,
    @PhoneNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Profiles
    WHERE (@ProfileId IS NULL OR @ProfileId = 0 OR ProfileId = @ProfileId)
      AND (@FirstName IS NULL OR @FirstName = '' OR FirstName = @FirstName)
      AND (@LastName IS NULL OR @LastName = '' OR LastName = @LastName)
      AND (@DateOfBirth IS NULL OR DateOfBirth = @DateOfBirth)
      AND (@PhoneNumber IS NULL OR @PhoneNumber = '' OR PhoneNumber = @PhoneNumber)
      AND (@EmailId IS NULL OR @EmailId = '' OR EmailId = @EmailId);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_GetFilteredTasks]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetFilteredTasks]
    @ProfileId INT = NULL,
    @TaskName NVARCHAR(50) = NULL,
    @TaskDescription NVARCHAR(50) = NULL,
    @StartTime DATETIME = NULL,
    @Status INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT t.Id, t.ProfileId, t.TaskName, t.TaskDescription, t.StartTime, t.Status, p.FirstName,p.LastName,p.ProfileId
    FROM dbo.Tasks t
    JOIN Profiles p ON t.ProfileId = p.ProfileId
    WHERE (@ProfileId IS NULL OR @ProfileId = 0 OR t.ProfileId = @ProfileId)
      AND (@TaskName IS NULL OR @TaskName = '' OR t.TaskName = @TaskName)
      AND (@TaskDescription IS NULL OR @TaskDescription = '' OR t.TaskDescription = @TaskDescription)
      AND (@StartTime IS NULL OR t.StartTime = @StartTime)
      AND (@Status IS NULL OR @Status = 0 OR t.Status = @Status);
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertOrUpdateTask]    Script Date: 7/20/2024 9:38:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertOrUpdateTask]
    @Id INT = NULL, -- Use NULL for insert, provide Id for update
    @ProfileId INT = NULL,
    @TaskName NVARCHAR(50) = NULL,
    @TaskDescription NVARCHAR(50) = NULL,
    @StartTime DATETIME = NULL,
    @Status INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @Id IS NULL
    BEGIN
        -- Insert new record
        INSERT INTO Tasks(ProfileId, TaskName, TaskDescription, StartTime, Status)
        VALUES (@ProfileId, @TaskName, @TaskDescription, @StartTime, @Status);
    END
    ELSE
    BEGIN
        -- Update existing record
        UPDATE Tasks
        SET ProfileId = @ProfileId,
            TaskName = @TaskName,
            TaskDescription = @TaskDescription,
            StartTime = @StartTime,
            Status = @Status
        WHERE Id = @Id;
    END
END;
GO
USE [master]
GO
ALTER DATABASE [PilotTasksDB] SET  READ_WRITE 
GO





The Co
