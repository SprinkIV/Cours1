CREATE TABLE [dbo].[Magasin] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Libelle]  NVARCHAR (200) NULL,
    [ImportId] BIGINT         NULL,
    CONSTRAINT [PK_Magasin] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UX_Magasin_Libelle]
    ON [dbo].[Magasin]([Libelle] ASC);

