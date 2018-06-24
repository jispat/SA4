CREATE TABLE [dbo].[User] (
    [UserId]    UNIQUEIDENTIFIER CONSTRAINT [DF_User_UserId] DEFAULT (newid()) NOT NULL,
    [UserName]  NVARCHAR (25)    NOT NULL,
    [FullName]  NVARCHAR (50)    NOT NULL,
    [Mobile]    NVARCHAR (15)    NOT NULL,
    [Email]     NVARCHAR (100)   NOT NULL,
    [Password]  NVARCHAR (100)   NOT NULL,
    [Code]      NVARCHAR (50)    NULL,
    [IsActive]  BIT              CONSTRAINT [DF_User_IsActive] DEFAULT ((0)) NULL,
    [CreatedOn] DATETIME         CONSTRAINT [DF_User_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [UpdatedOn] DATETIME         NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

