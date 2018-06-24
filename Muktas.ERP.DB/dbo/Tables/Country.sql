CREATE TABLE [dbo].[Country] (
    [CountryId]   UNIQUEIDENTIFIER CONSTRAINT [DF_Country_CountryId] DEFAULT (newid()) NOT NULL,
    [CountryCode] NVARCHAR (2)     NOT NULL,
    [CountryName] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);

