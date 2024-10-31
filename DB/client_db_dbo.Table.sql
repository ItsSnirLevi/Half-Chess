CREATE TABLE [dbo].[TblGames]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PlayerID] INT NOT NULL, 
    [StartGameTime] DATETIME NOT NULL, 
    [GameDuration] FLOAT NOT NULL, 
    [GameMoves] TEXT NULL
)
