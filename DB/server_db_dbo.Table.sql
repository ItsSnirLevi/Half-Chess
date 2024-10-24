CREATE TABLE [dbo].[TblUsers] (
    [Id]        INT       NOT NULL PRIMARY KEY,
    [FirstName] CHAR (20) NOT NULL,
    [LastName]  CHAR (20) NULL,
    [Phone]     CHAR (10) NOT NULL,
    [Country]   CHAR (20) NOT NULL,
    [CreatedAt] DATETIME  NOT NULL,
);

