CREATE TABLE [dbo].[ArticleAttribut] (
    [Id]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [IdArticle]  BIGINT          NOT NULL,
    [IdAttribut] BIGINT          NOT NULL,
    [Valeur]     NVARCHAR (4000) NOT NULL,
    CONSTRAINT [PK_ArticleAttribut] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ArticleAttribut_Article] FOREIGN KEY ([IdArticle]) REFERENCES [dbo].[Article] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ArticleAttribut_Attribut] FOREIGN KEY ([IdAttribut]) REFERENCES [dbo].[Attribut] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_ArticleAttribut_IdArticle_IdAttribut]
    ON [dbo].[ArticleAttribut]([IdArticle] ASC, [IdAttribut] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ArticleAttribut_IdAttribut]
    ON [dbo].[ArticleAttribut]([IdAttribut] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ArticleAttribut_IdArticle]
    ON [dbo].[ArticleAttribut]([IdArticle] ASC);

