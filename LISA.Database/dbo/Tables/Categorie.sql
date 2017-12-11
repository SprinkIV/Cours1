CREATE TABLE [dbo].[Categorie] (
    [Id]      BIGINT         IDENTITY (1, 1) NOT NULL,
    [Libelle] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Categorie] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Categorie_Libelle]
    ON [dbo].[Categorie]([Libelle] ASC);

