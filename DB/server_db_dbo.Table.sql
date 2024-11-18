CREATE TABLE [dbo].[TblUsers] (
    [Id]          INT       NOT NULL,
    [FirstName]   CHAR (20) NOT NULL,
    [LastName]    CHAR (20) NULL,
    [Phone]       CHAR (10) NOT NULL,
    [Country]     CHAR (5)  NOT NULL,
    [CreatedAt]   DATETIME  NOT NULL,
    [LastPlayed]  DATETIME  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

