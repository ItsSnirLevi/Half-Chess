CREATE TABLE [dbo].[TblGames] (
    [Id]            INT        IDENTITY (1, 1) NOT NULL,
    [PlayerID]      INT        NOT NULL,
    [PlayerName]    CHAR (20)  NOT NULL,
    [StartGameTime] DATETIME   NOT NULL,
    [GameDuration]  FLOAT (53) NOT NULL,
    [GameMoves]     TEXT       NOT NULL,
    [IsWhite]   BIT        NOT NULL,
    [Winner]        CHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

