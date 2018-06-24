CREATE TABLE [dbo].[Log] (
    [LogId]       UNIQUEIDENTIFIER CONSTRAINT [DF_Log_LogId] DEFAULT (newid()) NOT NULL,
    [MessageType] NVARCHAR (10)    NOT NULL,
    [Message]     NVARCHAR (500)   NOT NULL,
    [URL]         NVARCHAR (500)   NOT NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF_Log_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NULL,
    [Description] NVARCHAR (4000)  NULL,
    [Data]        NVARCHAR (4000)  NULL,
    [IPAddress]   NVARCHAR (20)    NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

