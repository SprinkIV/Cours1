CREATE TABLE [dbo].[Article] (
    [Id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [IdCategory]  BIGINT          NULL,
    [Code]        NVARCHAR (50)   NOT NULL,
    [Libelle]     NVARCHAR (200)  NOT NULL,
    [Description] NVARCHAR (2000) NULL,
    [Quantite]    SMALLINT        NOT NULL,
    [Unite]       NVARCHAR (20)   NULL,
    [ImportId]    BIGINT          NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Article_Categorie] FOREIGN KEY ([IdCategory]) REFERENCES [dbo].[Categorie] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Article_IdCategory]
    ON [dbo].[Article]([IdCategory] ASC);

