CREATE TABLE [dbo].[EmailTemplate] (
    [EmailTemplateId] UNIQUEIDENTIFIER CONSTRAINT [DF_EmailTemplate_EmailTemplateId] DEFAULT (newid()) NOT NULL,
    [TemplateName]    NVARCHAR (50)    NOT NULL,
    [Subject]         NVARCHAR (500)   NOT NULL,
    [Body]            NTEXT            NOT NULL,
    CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC)
);

