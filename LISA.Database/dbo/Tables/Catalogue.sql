CREATE TABLE [dbo].[Catalogue] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [IdOperation] BIGINT         NOT NULL,
    [Type]        NVARCHAR (200) NULL,
    [Libelle]     NVARCHAR (200) NULL,
    [Vitesse]     NVARCHAR (20)  NOT NULL,
    [Largeur]     INT            NOT NULL,
    [Hauteur]     INT            NOT NULL,
    [ImportId]    BIGINT         NULL,
    CONSTRAINT [PK_Catalogue] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Catalogue_Operation] FOREIGN KEY ([IdOperation]) REFERENCES [dbo].[Operation] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Catalogue_IdOperation]
    ON [dbo].[Catalogue]([IdOperation] ASC);

