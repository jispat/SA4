CREATE TABLE [dbo].[Token] (
    [TokenId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Token_TokenId] DEFAULT (newid()) NOT NULL,
    [UserId]    UNIQUEIDENTIFIER NOT NULL,
    [AuthToken] NVARCHAR (100)   NOT NULL,
    [IssuedOn]  DATETIME         CONSTRAINT [DF_Token_IssuedOn] DEFAULT (getdate()) NOT NULL,
    [ExpiresOn] DATETIME         NOT NULL,
    CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED ([TokenId] ASC)
);

