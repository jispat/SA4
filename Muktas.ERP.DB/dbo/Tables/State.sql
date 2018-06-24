CREATE TABLE [dbo].[State] (
    [StateId]       UNIQUEIDENTIFIER CONSTRAINT [DF_State_StateId] DEFAULT (newid()) NOT NULL,
    [StateCode]     NVARCHAR (2)     NOT NULL,
    [StateName]     NVARCHAR (50)    NOT NULL,
    [CountryId]     UNIQUEIDENTIFIER NOT NULL,
    [StateVatTinNo] INT              NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateId] ASC)
);

