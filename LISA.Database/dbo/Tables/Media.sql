CREATE TABLE [dbo].[Media] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [IdTypeMedia] BIGINT          NOT NULL,
    [IdArticle]   BIGINT          NOT NULL,
    [Chemin]      NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_Media] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Media_Article] FOREIGN KEY ([IdArticle]) REFERENCES [dbo].[Article] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Media_TypeMedia] FOREIGN KEY ([IdTypeMedia]) REFERENCES [dbo].[TypeMedia] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Media_IdTypeMedia]
    ON [dbo].[Media]([IdTypeMedia] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Media_IdArticle]
    ON [dbo].[Media]([IdArticle] ASC);

