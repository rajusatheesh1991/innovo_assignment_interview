USE [InnovoAssignmentDb]
GO
/****** Object:  Table [dbo].[UserAddresses]    Script Date: 5/3/2021 7:38:36 AM ******/
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
/****** Object:  Table [dbo].[UserPreferences]    Script Date: 5/3/2021 7:38:36 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 5/3/2021 7:38:36 AM ******/
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
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
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
/****** Object:  StoredProcedure [dbo].[Address_Create_Update]    Script Date: 5/3/2021 7:38:36 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Pref_Create_Update]    Script Date: 5/3/2021 7:38:36 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Users_Signup]    Script Date: 5/3/2021 7:38:36 AM ******/
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
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 5/3/2021 7:38:36 AM ******/
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
