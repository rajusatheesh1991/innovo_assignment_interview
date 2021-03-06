USE [InnovoAssignmentDb]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/3/2021 4:58:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](60) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](10) NULL,
	[Password] [nvarchar](250) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
	[IsAccountVerified] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPreferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EnablePromotionNotifications] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserPreferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAddresses]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAddresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[City] [nvarchar](60) NOT NULL,
	[State] [nvarchar](60) NOT NULL,
	[ZipCode] [nvarchar](6) NOT NULL,
	[Country] [nvarchar](60) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ModifiedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetProfileQuery]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetProfileQuery]
AS
SELECT        dbo.Users.FullName, dbo.Users.Email, dbo.Users.PhoneNumber, dbo.Users.Id, dbo.UserAddresses.AddressLine1, dbo.UserAddresses.AddressLine2, dbo.UserAddresses.City, dbo.UserAddresses.State, 
                         dbo.UserAddresses.ZipCode, dbo.UserAddresses.Country, dbo.UserPreferences.EnablePromotionNotifications
FROM            dbo.Users LEFT OUTER JOIN
                         dbo.UserAddresses ON dbo.Users.Id = dbo.UserAddresses.UserId LEFT OUTER JOIN
                         dbo.UserPreferences ON dbo.Users.Id = dbo.UserPreferences.UserId
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsAccountVerified]
GO
ALTER TABLE [dbo].[UserAddresses]  WITH CHECK ADD  CONSTRAINT [FK_UserAddresses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAddresses] CHECK CONSTRAINT [FK_UserAddresses_Users_UserId]
GO
ALTER TABLE [dbo].[UserPreferences]  WITH CHECK ADD  CONSTRAINT [FK_UserPreferences_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPreferences] CHECK CONSTRAINT [FK_UserPreferences_Users_UserId]
GO
/****** Object:  StoredProcedure [dbo].[Address_Create_Update]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Address_Create_Update]
  
  @Action VARCHAR(100) = NULL,
   @Id int = NULL,
       @UserId int = NULL,
      @AddrLine1 VARCHAR(60) = NULL,
	   
     @AddrLine2 VARCHAR(60) = NULL,
	   @City VARCHAR(60) = NULL,
	     @State VARCHAR(60) = NULL,
		   @ZipCode VARCHAR(6) = NULL,
		     @Country VARCHAR(60) = NULL,
	  @RID    INT    OUTPUT
	  
AS
BEGIN
      SET NOCOUNT ON;
 
     
 
    IF (@Action = 'INSERT')
      
      BEGIN
            INSERT INTO UserAddresses ([UserId]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[City]
           ,[State]
           ,[ZipCode]
           ,[Country]
           ,[CreatedAt]
           ,[ModifiedAt])
            VALUES (@UserId,@AddrLine1, @AddrLine2,@City,@State,@ZipCode,@Country,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP)
			 SELECT   @RID  = SCOPE_IDENTITY()

           SELECT   @RID  AS id

         RETURN
			
      END
  ELSE BEGIN

       update UserAddresses set [UserId]=@UserId
           ,[AddressLine1]=@AddrLine1
           ,[AddressLine2]=@AddrLine2
           ,[City]=@City
           ,[State]=@State
           ,[ZipCode]=@ZipCode
           ,[Country]=@Country
           ,[CreatedAt]=CURRENT_TIMESTAMP
           ,[ModifiedAt]=CURRENT_TIMESTAMP
            
			 where Id=@Id
			 SET   @RID  = @@ROWCOUNT

    END
    
 
   
END
GO
/****** Object:  StoredProcedure [dbo].[GetProfileSp]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProfileSp] 
	-- Add the parameters for the stored procedure here
      @Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT        dbo.Users.FullName, dbo.Users.Email, dbo.Users.PhoneNumber, dbo.Users.Id, dbo.UserAddresses.AddressLine1, dbo.UserAddresses.AddressLine2, dbo.UserAddresses.City, dbo.UserAddresses.State, 
                         dbo.UserAddresses.ZipCode, dbo.UserAddresses.Country, dbo.UserPreferences.EnablePromotionNotifications
FROM            dbo.Users INNER JOIN
                         dbo.UserAddresses ON dbo.Users.Id = dbo.UserAddresses.UserId INNER JOIN
                         dbo.UserPreferences ON dbo.Users.Id = dbo.UserPreferences.UserId
						 where dbo.Users.Id=@Id
END
GO
/****** Object:  StoredProcedure [dbo].[Pref_Create_Update]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Pref_Create_Update]
  
  @Action VARCHAR(100) = NULL,
   @Id int = NULL,
       @UserId int = NULL,
      @Enable bit = NULL,
	  @RID    INT    OUTPUT
	  
AS
BEGIN
      SET NOCOUNT ON;
 
     
 
    IF (@Action = 'INSERT')
      
      BEGIN
            INSERT INTO UserPreferences([UserId]
           ,[EnablePromotionNotifications]
           ,[CreatedAt]
           ,[ModifiedAt])
            VALUES (@UserId,@Enable,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP)
			 SELECT   @RID  = SCOPE_IDENTITY()

           SELECT   @RID  AS id

         RETURN
			
      END
  ELSE BEGIN

       update UserPreferences set [UserId]=@UserId
           ,[EnablePromotionNotifications]=@Enable
           
           ,[ModifiedAt]=CURRENT_TIMESTAMP
            
			 where Id=@Id
			 SET   @RID  = @@ROWCOUNT

    END
    
 
   
END
GO
/****** Object:  StoredProcedure [dbo].[User_Authenticate]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[User_Authenticate]

 @Email VARCHAR(100) = NULL
	   
      ,@Password VARCHAR(250) = NULL,
	  @RID    INT    OUTPUT

AS    
 
   SET NOCOUNT ON;  
   SELECT @RID=ID
   FROM Users
   WHERE Email=@Email and Password=@Password
   
   
RETURN  
GO
/****** Object:  StoredProcedure [dbo].[Users_Signup]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Users_Signup]
  
      
      @Email VARCHAR(100) = NULL
	   
      ,@Password VARCHAR(250) = NULL,
	  @RID    INT    OUTPUT
	  
AS
BEGIN
      SET NOCOUNT ON;
 
     
 
     
      
      BEGIN
            INSERT INTO Users(Email, Password,CreatedAt,ModifiedAt)
            VALUES (@Email, @Password,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP)
			 SELECT   @RID  = SCOPE_IDENTITY()

    SELECT   @RID  AS id

    RETURN
			
      END
 
    
 
   
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 5/3/2021 4:58:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Users_Update]
  
       @Id VARCHAR(100) =  NULL,
      @Name VARCHAR(100) = NULL
	   
      ,@Phone VARCHAR(10) = NULL,
	  @ROWCOUNT    INT    OUTPUT
	  
AS
BEGIN
      SET NOCOUNT ON;
 
     
 
     
      
      BEGIN
            update  Users set FullName= @Name, PhoneNumber=@Phone,ModifiedAt=CURRENT_TIMESTAMP
             where Id=@Id
			 SET   @ROWCOUNT  = @@ROWCOUNT


    RETURN
			
      END
 
    
 
   
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Users"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserAddresses"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "UserPreferences"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 136
               Right = 703
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetProfileQuery'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetProfileQuery'
GO
