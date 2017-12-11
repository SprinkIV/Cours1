CREATE TABLE [dbo].[PageArticle] (
    [Id]               BIGINT IDENTITY (1, 1) NOT NULL,
    [IdPage]           BIGINT NOT NULL,
    [IdArticle]        BIGINT NOT NULL,
    [ZoneHauteur]      INT    NOT NULL,
    [ZoneLargeur]      INT    NOT NULL,
    [ZoneCoordonnéesX] INT    NOT NULL,
    [ZoneCoordonnéesY] INT    NOT NULL,
    CONSTRAINT [PK_PageArticle] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PageArticle_Article] FOREIGN KEY ([IdArticle]) REFERENCES [dbo].[Article] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_PageArticle_Page] FOREIGN KEY ([IdPage]) REFERENCES [dbo].[Page] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PageArticle_IdPage]
    ON [dbo].[PageArticle]([IdPage] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PageArticle_IdArticle]
    ON [dbo].[PageArticle]([IdArticle] ASC);

